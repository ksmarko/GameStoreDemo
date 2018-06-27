using FluentAssertions;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Controllers;
using GameStore.WEB.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace GameStore.WEB.Tests.Controllers
{
    [TestFixture]
    public class GenresControllerTests
    {
        #region Fields & Init

        private Mock<IGenreService> _genreService;
        private GenresController _genresController;

        static GenresControllerTests()
        {
            Configuration.AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _genreService = new Mock<IGenreService>();

            _genresController = new GenresController(_genreService.Object);
        }

        #endregion

        [Test]
        public void GenresController_should_throw_ArgumentNullException_if_input_service_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _genresController = new GenresController(null));
        }

        [Test]
        public void CreateGenre_should_create_genre()
        {
            //Act
            var actionResult = _genresController.CreateGenre(new GenreModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void Get_should_return_genre_by_id()
        {
            //Arrange
            int id = It.IsAny<int>();
            _genreService.Setup(x => x.Get(It.IsAny<int>())).Returns(new GenreDTO() { Id = id });

            //Act
            var actionResult = _genresController.GetGenre(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<GenreModel>();
            Assert.AreEqual(id, actionResult.Id);
        }

        [Test]
        public void EditGenre_should_edit_genre()
        {
            //Act
            var actionResult = _genresController.EditGenre(It.IsAny<int>(), new GenreModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void DeleteGenre_should_delete_genre()
        {
            //Act
            var actionResult = _genresController.DeleteGenre(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void GetGenres_should_return_all_genres()
        {
            //Arrange
            _genreService.Setup(x => x.GetAll()).Returns(new List<GenreDTO>() { new GenreDTO() });

            //Act
            var actionResult = _genresController.GetGenres();

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }

        [Test]
        public void GetGames_should_return_list_of_games_for_genre()
        {
            //Arrange
            _genreService.Setup(x => x.Get(It.IsAny<int>())).Returns(new GenreDTO() { Games = new List<GameDTO>() { It.IsAny<GameDTO>() } });

            //Act
            var actionResult = _genresController.GetGames(It.IsAny<int>());

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }
    }
}
