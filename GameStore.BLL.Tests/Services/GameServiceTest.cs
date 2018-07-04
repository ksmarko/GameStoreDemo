using FluentAssertions;
using GameStore.BLL.DTO;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Tests.Configuration;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Tests.Services
{
    [TestFixture]
    public class GameServiceTest
    {
        #region Fields & Init

        private Mock<IUnitOfWork> _uow;
        private Mock<IRepository<Game>> _gameRepository;
        private Mock<IRepository<Publisher>> _publisherRepository;
        private Mock<IRepository<Genre>> _genreRepository;
        private Mock<IRepository<PlatformType>> _platformRepository;
        private IGameService _gameService;

        static GameServiceTest()
        {
            AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _uow = new Mock<IUnitOfWork>();

            _gameRepository = new Mock<IRepository<Game>>();
            _publisherRepository = new Mock<IRepository<Publisher>>();
            _genreRepository = new Mock<IRepository<Genre>>();
            _platformRepository = new Mock<IRepository<PlatformType>>();

            _uow.Setup(x => x.Games).Returns(_gameRepository.Object);
            _uow.Setup(x => x.Publishers).Returns(_publisherRepository.Object);
            _uow.Setup(x => x.Genres).Returns(_genreRepository.Object);
            _uow.Setup(x => x.PlatformTypes).Returns(_platformRepository.Object);

            _gameService = new GameService(_uow.Object);
        }

        #endregion

        #region GameService

        [Test]
        public void GameService_should_throw_ArgumentNullException_when_unit_of_work_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService = new GameService(null));
        }

        #endregion

        #region Create

        [Test]
        public void Create_should_throw_ArgumentNullException_when_input_entity_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.Create(null));
        }

        [Test]
        public void Create_should_throw_PublisherNotFoundException_if_publisher_does_not_exist_in_the_database()
        {
            //Arrange
            var game = new GameDTO() {Name = "Game", Publisher = "Publisher"};
            _publisherRepository.Setup(x => x.GetAll()).Returns(new List<Publisher>());

            //Act & Assert
            Assert.Throws<PublisherNotFoundException>(() => _gameService.Create(game));
        }

        [Test]
        public void Create_repository_should_be_created_once()
        {
            //Arrange
            var game = new GameDTO() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>() };
            _publisherRepository.Setup(x => x.Find(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>() { new Publisher() { Name = It.IsAny<string>() } });

            //Act
            _gameService.Create(game);

            //Assert
            _gameRepository.Verify(x => x.Create(It.IsAny<Game>()), Times.Once);
        }

        [Test]
        public void Create_should_create_game_if_publisher_exists_in_the_database()
        {
            //Arrange
            var game = new GameDTO() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>() };
            _publisherRepository.Setup(x => x.Find(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>() { new Publisher() { Name = It.IsAny<string>() } });

            //Act
            _gameService.Create(game);

            //Assert
            _gameRepository.Verify(x => x.Create(It.IsAny<Game>()));
        }
        #endregion

        #region Edit

        [Test]
        public void Edit_should_throw_ArgumentNullException_if_input_entity_is_null()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.Edit(null));
        }

        [Test]
        public void Edit_should_throw_ArgumentNullException_if_game_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            var game = new GameDTO() { Name = It.IsAny<string>()};
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Game>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.Edit(game));
        }

        [Test]
        public void Edit_should_edit_game()
        {
            //Arrange
            var game = new GameDTO() { Name = It.IsAny<string>()};
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game() {Name = It.IsAny<string>()});

            //Act
            _gameService.Edit(game);

            //Assert
            _gameRepository.Verify(x => x.Update(It.IsAny<Game>()));
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_should_throw_ArgumentNullException_if_game_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Game>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.Delete(It.IsAny<int>()));
        }

        [Test]
        public void Delete_should_delete_game()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game() { Name = It.IsAny<string>() });

            //Act
            _gameService.Delete(It.IsAny<int>());

            //Assert
            _gameRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_should_return_list_of_games()
        {
            //Arrange
            _gameRepository.Setup(x => x.GetAll()).Returns(new List<Game>());

            //Act
            var games = _gameService.GetAll();

            //Assert
            games.Should().NotBeNull();
        }

        [Test]
        public void GetAll_should_return_empty_list_if_games_do_not_exist()
        {
            //Arrange
            _gameRepository.Setup(x => x.GetAll()).Returns<IEnumerable<Game>>(null);

            //Act
            var games = _gameService.GetAll();

            //Assert
            games.Should().NotBeNull();
        }

        #endregion

        #region GetByGenre

        [Test]
        public void GetByGenre_should_throw_ArgumentNullException_if_genre_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Genre>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.GetByGenre(It.IsAny<int>()));
        }

        [Test]
        public void GetByGenre_should_return_games_with_specified_genre()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Genre() {Name = "Genre"});
            _gameRepository.Setup(x => x.Find(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>() { new Game() { Name = It.IsAny<string>() } });

            //Act
            _gameService.GetByGenre(It.IsAny<int>());

            //Assert
            _gameRepository.Verify(x => x.Find(It.IsAny<Func<Game, bool>>()));
        }

        #endregion

        #region GetByPlatformType

        [Test]
        public void GetByPlatformType_should_throw_ArgumentNullException_if_platform_type_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _platformRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<PlatformType>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.GetByPlatformType(It.IsAny<int>()));
        }

        [Test]
        public void GetByPlatformType_should_return_games_with_specified_platform_type()
        {
            //Arrange
            _platformRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new PlatformType() { Type = "Platform" });
            _gameRepository.Setup(x => x.Find(It.IsAny<Func<Game, bool>>())).Returns(new List<Game>() { new Game() { Name = It.IsAny<string>() } });

            //Act
            _gameService.GetByPlatformType(It.IsAny<int>());

            //Assert
            _gameRepository.Verify(x => x.Find(It.IsAny<Func<Game, bool>>()));
        }

        #endregion

        #region Get

        [Test]
        public void Get_should_throw_ArgumentNullException_if_game_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Game>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _gameService.Get(It.IsAny<int>()));
        }

        [Test]
        public void Get_should_get_game_by_id()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game() { Name = It.IsAny<string>() });

            //Act
            var game = _gameService.Get(It.IsAny<int>());

            //Assert
            _gameRepository.Verify(x => x.Get(It.IsAny<int>()));
        }

        [Test]
        public void Get_should_increment_views()
        {
            //Arrange
            var views = 1;
            var incrementedViews = 2;
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game() { Name = It.IsAny<string>(), Views = views});

            //Act
            var game = _gameService.Get(It.IsAny<int>());

            //Assert
            Assert.AreEqual(incrementedViews, game.Views);
        }

        #endregion
    }
}
