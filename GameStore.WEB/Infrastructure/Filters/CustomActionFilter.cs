using GameStore.WEB.Infrastructure;
using NLog;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GameStore.WEB.Filters
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _log;

        public CustomActionFilter(ILogger log)
        {
            _log = log;
        }

        public override bool AllowMultiple => false;

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = actionContext.Request;

                _log.Info(LogMessageComposer.Compose(
                    new
                    {
                        details = "Action executing started",
                        method = request.Method,
                        url = request.RequestUri,
                        userIP = ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress
                    }));
            });
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _log.Info(LogMessageComposer.Compose(
                    new
                    {
                        details = "Action finished"
                    }));
            });
        }
    }
}