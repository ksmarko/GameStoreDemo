using GameStore.WEB.Infrastructure;
using GameStore.WEB.Infrastructure.Exceptions;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using GameStore.BLL.Exceptions;

namespace GameStore.WEB.Filters
{
    public class CustomErrorFilter : IExceptionFilter
    {
        private readonly ILogger _log;

        public CustomErrorFilter(ILogger log)
        {
            _log = log;
        }

        public bool AllowMultiple => true;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                WrapAndLogException(context);

            }, cancellationToken);
        }

        private void WrapAndLogException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            HttpStatusCode code;
            string message;

            if (exception is CustomApiException customException)
            {
                code = customException.Code;
                message = JsonConvert.SerializeObject(new { Reason = customException.Message, customException.Fields });
            }
            else
            {
                if (exception is ItemNotFoundException || exception is PublisherNotFoundException)
                    code = HttpStatusCode.NotFound;
                else 
                    code = HttpStatusCode.InternalServerError;

                var r = context.Request;
                var headers = string.Join(Environment.NewLine, r.Headers.Select(x => $"{x.Key}:{x.Value}"));

                _log.Error(exception,
                    LogMessageComposer.Compose(
                    new
                    {
                        details = "Http request failed",
                        user = "Anonimous",
                        url = r.RequestUri,
                        headers
                        //TODO: Log here other usefull information which can be retrieved.
                    }));

                message = JsonConvert.SerializeObject(new { Reason = "An error occurred. Please try again." });
            }

            context.Response = context.Request.CreateErrorResponse(code, message);
        }
    }
}