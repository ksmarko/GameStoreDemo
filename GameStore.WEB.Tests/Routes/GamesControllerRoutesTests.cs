using GameStore.WEB.Controllers;
using GameStore.WEB.Models;
using GameStore.WEB.Tests.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.WEB.Tests.Routes
{
    public class GamesControllerRoutesTests
    {
        private readonly HttpConfiguration _config;
        private readonly string _prefix = "http://localhost:58326/";

        public GamesControllerRoutesTests()
        {
            _config = new HttpConfiguration {IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always};
            _config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}/{action}", defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional });
        }

        [Test]
        public void POST_method_has_correct_destination_CreateGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _prefix + "api/games");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.CreateGame(It.IsAny<AddGameModel>())), routeTester.GetActionName());
        }

        [Test]
        public void PUT_method_has_correct_destination_EditGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _prefix + "api/games/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.EditGame(It.IsAny<int>(), It.IsAny<EditGameModel>())), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/games/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.GetGame(1)), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_DownloadGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/games/1/download");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.DownloadGame(1)), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetGenresForGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/games/1/genres");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.GetGenresForGame(1)), routeTester.GetActionName());
        }

        [Test]
        public void DELETE_method_has_correct_destination_DeleteGame()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _prefix + "api/games/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.DeleteGame(It.IsAny<int>())), routeTester.GetActionName());
        }
    }
}
