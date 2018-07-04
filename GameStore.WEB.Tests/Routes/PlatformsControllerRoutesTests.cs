using GameStore.WEB.Controllers;
using GameStore.WEB.Tests.Infrastructure;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.WEB.Tests.Routes
{
    public class PlatformsControllerRoutesTests
    {
        private readonly HttpConfiguration _config;
        private readonly string _prefix = "http://localhost:58326/";

        public PlatformsControllerRoutesTests()
        {
            _config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
            _config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}/{action}", defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional });
        }

        [Test]
        public void GET_method_has_correct_destination_GetGenres()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/platforms");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PlatformsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((PlatformsController p) => p.GetPlatforms()), routeTester.GetActionName());
        }
    }
}
