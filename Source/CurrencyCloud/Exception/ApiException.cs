﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace CurrencyCloud.Exception
{
    /// <summary>
    /// Represents errors that occur when making API calls.
    /// </summary>
    public class ApiException : System.Exception
    {
        private readonly string yamlString;

        private string CreateYamlString()
        {
            StringBuilder yamlBuilder = new StringBuilder();

            yamlBuilder.AppendLine(GetType().Name);
            yamlBuilder.AppendLine("---");

            yamlBuilder.AppendFormat("platform: {0}", Platform);
            yamlBuilder.AppendLine();

            yamlBuilder.AppendLine("request:");
            yamlBuilder.Append("  parameters:");
            if (Request.Parameters.Count == 0)
            {
                yamlBuilder.Append(" {}");
            }
            else
            {
                foreach (var parameter in Request.Parameters)
                {
                    yamlBuilder.AppendLine();
                    yamlBuilder.AppendFormat("    {0}: {1}", parameter.Key, parameter.Value);
                }
            }
            yamlBuilder.AppendLine();
            yamlBuilder.AppendFormat("  verb: {0}", Request.Verb);
            yamlBuilder.AppendLine();
            yamlBuilder.AppendFormat("  url: {0}", Request.Url);
            yamlBuilder.AppendLine();

            yamlBuilder.AppendLine("response:");
            yamlBuilder.AppendFormat("  status_code: {0}", Response.StatusCode);
            yamlBuilder.AppendLine();
            yamlBuilder.AppendFormat("  date: {0}", Response.Date.ToUniversalTime().ToString("r"));
            yamlBuilder.AppendLine();
            yamlBuilder.AppendFormat("  request_id: {0}", Response.RequestId);
            yamlBuilder.AppendLine();

            yamlBuilder.Append("errors:");
            foreach (var error in Errors)
            {
                foreach (var errorMessage in error.ErrorMessages)
                {
                    yamlBuilder.AppendLine();
                    yamlBuilder.AppendFormat("- field: {0}", error.Field);
                    yamlBuilder.AppendLine();
                    yamlBuilder.AppendFormat("  code: {0}", errorMessage.Code);
                    yamlBuilder.AppendLine();
                    yamlBuilder.AppendFormat("  message: {0}", errorMessage.Message);
                    yamlBuilder.AppendLine();
                    yamlBuilder.Append("  params:");
                    if (errorMessage.Params.Count == 0)
                    {
                        yamlBuilder.Append(" {}");
                    }
                    else
                    {
                        foreach (var param in errorMessage.Params)
                        {
                            yamlBuilder.AppendLine();
                            yamlBuilder.AppendFormat("    {0}: {1}", param.Key, param.Value);
                        }
                    }
                }
            }

            return yamlBuilder.ToString();
        }

        protected ApiException(Request request, Response response, List<Error> errors)
        {
            Platform = Assembly
                .GetEntryAssembly()?
                .GetCustomAttribute<TargetFrameworkAttribute>()?
                .FrameworkName;
            Request = request;
            Response = response;
            Errors = errors;

            yamlString = CreateYamlString();
        }

        public readonly string Platform;
        public readonly Request Request;
        public readonly Response Response;
        public readonly List<Error> Errors;

        public string ToYamlString()
        {
            return yamlString;
        }

        public override string Message
        {
            get
            {
                return yamlString;
            }
        }
    }

    public class BadRequestException : ApiException
    {
        public BadRequestException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class AuthenticationException : ApiException
    {
        public AuthenticationException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class ForbiddenException : ApiException
    {
        public ForbiddenException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class TooManyRequestsException : ApiException
    {
        public TooManyRequestsException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class InternalApplicationException : ApiException
    {
        public InternalApplicationException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    public class UndefinedException : ApiException
    {
        public UndefinedException(Request request, Response response, List<Error> errors) : base(request, response, errors) { }
    }

    /// <summary>
    /// Represents request parameters of the HTTP call that caused API exception.
    /// </summary>
    public class Request
    {
        public readonly Dictionary<string, string> Parameters;
        public readonly string Verb;
        public readonly string Url;

        public Request(Dictionary<string, string> parameters, string verb, string url)
        {
            Parameters = parameters;
            Verb = verb;
            Url = url;
        }
    }

    /// <summary>
    /// Represents response parameters of the HTTP call that caused API exception.
    /// </summary>
    public class Response
    {
        public readonly int StatusCode;
        public readonly DateTime Date;
        public readonly string RequestId;

        public Response(int statusCode, DateTime date, string requestId)
        {
            StatusCode = statusCode;
            Date = date;
            RequestId = requestId;
        }
    }

    /// <summary>
    /// Represents API error.
    /// </summary>
    public class Error
    {
        public readonly string Field;
        public readonly List<ErrorMessage> ErrorMessages;

        public Error(string field, List<ErrorMessage> errorMessages)
        {
            Field = field;
            ErrorMessages = errorMessages;
        }

        /// <summary>
        /// Represents API error message.
        /// </summary>
        public class ErrorMessage
        {
            public readonly string Code;
            public readonly string Message;
            public readonly Dictionary<string, string> Params;

            public ErrorMessage(string code, string message, Dictionary<string, string> @params)
            {
                Code = code;
                Message = message;
                Params = @params;
            }
        }
    }
}
