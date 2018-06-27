﻿using FluentAssertions;
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
    public class GamesControllerTests
    {
        #region Fields & Init

        private Mock<IGameService> _gameService;
        private GamesController _gamesController;

        static GamesControllerTests()
        {
            Configuration.AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _gameService = new Mock<IGameService>();

            _gamesController = new GamesController(_gameService.Object);
        }

        #endregion

        [Test]
        public void GamesController_should_throw_ArgumentNullException_if_input_service_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gamesController = new GamesController(null));
        }

        [Test]
        public void CreateGame_should_create_game()
        {
            //Act
            var actionResult = _gamesController.CreateGame(new GameModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void Get_should_return_game_by_id()
        {
            //Arrange
            int id = It.IsAny<int>();
            _gameService.Setup(x => x.Get(It.IsAny<int>())).Returns(new GameDTO() {Id = id});

            //Act
            var actionResult = _gamesController.GetGame(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<GameModel>();
            Assert.AreEqual(id, actionResult.Id);
        }

        [Test]
        public void EditGame_should_edit_game()
        {
            //Act
            var actionResult = _gamesController.EditGame(It.IsAny<int>(), new EditGameModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void DeleteGame_should_delete_game()
        {
            //Act
            var actionResult = _gamesController.DeleteGame(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void GetGames_should_return_all_games()
        {
            //Arrange
            _gameService.Setup(x => x.GetAll()).Returns(new List<GameDTO>() { new GameDTO() });

            //Act
            var actionResult = _gamesController.GetGames();

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }

        [Test]
        public void GetGenresForGame_should_return_list_of_genres_for_game()
        {
            //Arrange
            _gameService.Setup(x => x.Get(It.IsAny<int>())).Returns(new GameDTO() {Genres = new List<string>() {It.IsAny<string>()}});

            //Act
            var actionResult = _gamesController.GetGenresForGame(It.IsAny<int>());

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }
    }
}