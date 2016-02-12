using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CurrencyCloud.Tests.Mock.Http
{
    class Player
    {
        private HttpListener listener;
        private Dictionary<string, Queue> recordingsSet;
        private Queue recordings;

        public Player(string path)
        {
            recordingsSet = new Dictionary<string, Queue>();

            var recs = File.ReadAllText(path);
            foreach (var rec in JArray.Parse(recs))
            {
                var name = rec["name"].ToString();

                var recordings = new Queue();
                foreach (var request in rec["requests"])
                {
                    recordings.Enqueue(request);
                }

                recordingsSet.Add(name, recordings);
            }
        }

        public void Play(string name)
        {
            recordings = recordingsSet[name];
        }

        public void Start(string baseUrl)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/");
            listener.Start();

            Task.Factory.StartNew(() =>
            {
                while (listener.IsListening)
                {
                    try
                    {
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;

                        dynamic recording = recordings.Dequeue();

                        System.Collections.Specialized.NameValueCollection requestParams = System.Web.HttpUtility.ParseQueryString(request.Url.Query);
                        System.Collections.Specialized.NameValueCollection recordingParams = System.Web.HttpUtility.ParseQueryString((string)(recording.request.query ?? ""));
                        bool isMathingQueryStrings = true;
                        foreach (string key in recordingParams)
                        {
                            isMathingQueryStrings = isMathingQueryStrings && requestParams[key] == recordingParams[key];
                            if (requestParams[key] != recordingParams[key])
                            {
                                throw new System.Exception(string.Format("Unexpected param value. Param name '{0}'. Expected '{1}' Received '{2}'",
                                    key, recordingParams[key], requestParams[key]));
                            }
                        }
                        foreach(string key in requestParams)
                            isMathingQueryStrings = isMathingQueryStrings && requestParams[key] == recordingParams[key];

                        bool isMatchingRequest = recording.request.method == request.HttpMethod &&
                                                 recording.request.path == request.Url.AbsolutePath &&
                                                 isMathingQueryStrings;
                        if (isMatchingRequest && recording.request.headers != null)
                        {
                            foreach (var header in recording.request.headers)
                            {
                                if (request.Headers.Get(header.Name) != header.Value.ToString())
                                {
                                    isMatchingRequest = false;
                                    break;
                                }
                            }
                        }
                        if (!isMatchingRequest)
                        {
                            throw new InvalidOperationException("Request is not recorded.");
                        }

                        response.StatusCode = recording.response.status;

                        if (recording.response.headers != null)
                        {
                            foreach (var header in recording.response.headers)
                            {
                                response.Headers.Add(header.Name, header.Value.ToString());
                            }
                        }

                        byte[] responseBuffer = Encoding.UTF8.GetBytes(recording.response.body.ToString());
                        response.ContentLength64 = responseBuffer.Length;

                        Stream responseStream = response.OutputStream;
                        responseStream.Write(responseBuffer, 0, responseBuffer.Length);
                        responseStream.Close();
                    }
                    catch (System.Exception ex)
                    {
                        Close();

                        throw ex;
                    }
                }
            });
        }

        public void Close()
        {
            if(listener != null)
            {
                listener.Stop();
                listener.Close();
            }
        }
    }
}
