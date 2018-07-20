using GameStore.WEB.Exceptions;
using GameStore.WEB.Helpers;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GameStore.WEB.Filters
{
    public class CustomValidationFilter : IActionFilter
    {
        private readonly ILogger _log;

        public CustomValidationFilter(ILogger log)
        {
            _log = log;
        }

        public bool AllowMultiple => true;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext context, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!context.ModelState.IsValid)
            {
                return Task.FromResult(WrapAndLogValidationError(context));
            }
            return continuation();
        }

        private HttpResponseMessage WrapAndLogValidationError(HttpActionContext actionContext)
        {
            var invalidProperties = actionContext.ModelState.Where(m => m.Value.Errors.Any());

            var validationErrorCollection = invalidProperties.Select(x =>
            {
                var errors = x.Value.Errors.Select((e => String.IsNullOrEmpty(e.ErrorMessage)
                        ? Regex.Replace(e.Exception.Message, @"\.(.*)", String.Empty)
                        : e.ErrorMessage));

                var prefix = String.IsNullOrEmpty(x.Key) ? "Summary" : x.Key;
                return new { key = prefix, value = errors };

            }).ToDictionary(x => x.key, x => x.value);

            _log.Warn(LogMessageComposer.Compose(
                new
                {
                    details = "Model validation failed",
                    user = "Anonymous",
                    modelState = validationErrorCollection
                }));

            throw new CustomApiException(HttpStatusCode.BadRequest, "Resource validation failed.", validationErrorCollection);
        }
    }
}