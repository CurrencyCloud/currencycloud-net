using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CurrencyCloud.Tests.Mock.Http
{
    class Player
    {
        private HttpListener listener;
        private Queue recordings;

        public Player(string path)
        {
            recordings = new Queue();

            var recs = File.ReadAllText(path);
            foreach (var rec in JArray.Parse(recs))
            {
                recordings.Enqueue(rec);
            }
        }

        public void Start(string baseUrl)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(baseUrl);
            listener.Start();

            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    dynamic recording = recordings.Dequeue();

                    bool isMatchingRequest = recording.request.method == request.HttpMethod ||
                                             recording.request.path == request.Url.AbsolutePath ||
                                             recording.request.query == request.Url.Query;
                    if(isMatchingRequest && recording.request.headers != null)
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
                        throw new InvalidOperationException("Request not found among recordings.");
                    }

                    response.StatusCode = recording.response.status;

                    if(recording.response.headers != null)
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
                catch (System.Exception)
                {
                    Stop();

                    throw;
                }
            }
        }

        public void Stop()
        {
            if(listener.IsListening)
            {
                listener.Stop();
            }
        }
    }
}
