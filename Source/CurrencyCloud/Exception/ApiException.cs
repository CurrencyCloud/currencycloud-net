using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CurrencyCloud.Exception
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiException : System.Exception
    {
        private readonly string yamlString;

        protected ApiException(Request request, Response response, List<Error> errors)
        {
            Platform = "";
            Request = request;
            Response = response;
            Errors = errors;

            yamlString = "";
        }

        public readonly string Platform;
        public readonly Request Request;
        public readonly Response Response;
        public readonly List<Error> Errors;

        public override string ToString()
        {
            return yamlString;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BadRequestException : ApiException
    {
        public BadRequestException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationException : ApiException
    {
        public AuthenticationException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : ApiException
    {
        public NotFoundException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TooManyRequestsException : ApiException
    {
        public TooManyRequestsException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InternalApplicationException : ApiException
    {
        public InternalApplicationException(Request request, Response response, List<Error> errors): base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UndefinedException : ApiException
    {
        public UndefinedException(Request request, Response response, List<Error> errors) : base(request, response, errors) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Request
    {
        public readonly NameValueCollection Parameters;
        public readonly string Verb;
        public readonly string Url;

        public Request(NameValueCollection parameters, string verb, string url)
        {
            Parameters = parameters;
            Verb = verb;
            Url = url;
        }
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public class Error
    {
        public readonly string Field;
        public readonly string Code;
        public readonly string Message;
        public readonly NameValueCollection Params;

        public Error(string field, string code, string message, NameValueCollection @params)
        {
            Field = field;
            Code = code;
            Message = message;
            Params = @params;
        }
    }
}
