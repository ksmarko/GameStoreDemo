using GameStore.WEB.Controllers;
using GameStore.WEB.Models;
using GameStore.WEB.Tests.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.WEB.Tests.Routes
{
    public class PublisherControllerRoutesTests
    {
        private readonly HttpConfiguration _config;
        private readonly string _prefix = "http://localhost:58326/";

        public PublisherControllerRoutesTests()
        {
            _config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
            _config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}/{action}", defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional });
        }

        [Test]
        public void POST_method_has_correct_destination_CreatePublisher()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _prefix + "api/publisher");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.CreatePublisher(It.IsAny<PublisherModel>())), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetPublishers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/publisher");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.GetPublishers()), routeTester.GetActionName());
        }

        [Test]
        public void PUT_method_has_correct_destination_EditPublisher()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _prefix + "api/publisher/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.EditPublisher(It.IsAny<int>(), It.IsAny<PublisherModel>())), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetPublisher()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/publisher/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.GetPublisher(1)), routeTester.GetActionName());
        }

        [Test] public void GET_method_has_correct_destination_GetGamesForPublisher()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/publisher/1/games");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.GetPublisherGames(1)), routeTester.GetActionName());
        }

        [Test]
        public void DELETE_method_has_correct_destination_DeletePublisher()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _prefix + "api/publisher/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(PublisherController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((PublisherController p) => p.DeletePublisher(It.IsAny<int>())), routeTester.GetActionName());
        }
    }
}
