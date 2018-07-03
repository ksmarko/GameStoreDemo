using GameStore.WEB.Filters;
using System;
using System.Web.Http;
using Newtonsoft.Json;

namespace GameStore.WEB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var logger = NLog.LogManager.GetLogger("GlobalApplicationLog");
            logger.Info("Configuring application started.");

            try
            {
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;


                GlobalConfiguration.Configure(WebApiConfig.Register);
                AutoMapperInitializer.Initialize();

                GlobalConfiguration.Configuration.Filters.Add(new CustomActionFilter(logger));
                GlobalConfiguration.Configuration.Filters.Add(new CustomValidationFilter(logger));
                GlobalConfiguration.Configuration.Filters.Add(new CustomErrorFilter(logger));
            }
            catch (Exception e)
            {
                logger.Fatal(e);
                throw;
            }

            logger.Info("Configuring application completed. Starting application.");
        }
    }
}
