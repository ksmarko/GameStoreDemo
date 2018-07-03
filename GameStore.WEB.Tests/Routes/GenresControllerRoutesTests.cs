using GameStore.WEB.Controllers;
using GameStore.WEB.Models;
using GameStore.WEB.Tests.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.WEB.Tests.Routes
{
    public class GenresControllerRoutesTests
    {
        private readonly HttpConfiguration _config;
        private readonly string _prefix = "http://localhost:58326/";

        public GenresControllerRoutesTests()
        {
            _config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
            _config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{id}/{action}", defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional });
        }

        [Test]
        public void POST_method_has_correct_destination_CreateGenre()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _prefix + "api/genres");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GenresController p) => p.CreateGenre(It.IsAny<GenreModel>())), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetGenres()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/genres");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GenresController p) => p.GetGenres()), routeTester.GetActionName());
        }

        [Test]
        public void PUT_method_has_correct_destination_EditGenre()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _prefix + "api/genres/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GenresController p) => p.EditGenre(It.IsAny<int>(), It.IsAny<GenreModel>())), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetGenre()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/genres/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GenresController p) => p.GetGenre(1)), routeTester.GetActionName());
        }

        [Test]
        public void GET_method_has_correct_destination_GetGamesForGenre()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _prefix + "api/genres/1/games");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            //Assert.AreEqual(ReflectionHelper.GetMethodName((GamesController p) => p.GetGenresForGame(1)), routeTester.GetActionName());
        }

        [Test]
        public void DELETE_method_has_correct_destination_DeleteGenre()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _prefix + "api/genres/1");

            var routeTester = new RouteTester(_config, request);

            Assert.AreEqual(typeof(GenresController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelper.GetMethodName((GenresController p) => p.DeleteGenre(It.IsAny<int>())), routeTester.GetActionName());
        }
    }
}
