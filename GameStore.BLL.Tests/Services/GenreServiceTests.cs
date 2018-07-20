using FluentAssertions;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Tests.Configuration;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Exceptions;

namespace GameStore.BLL.Tests.Services
{
    [TestFixture]
    public class GenreServiceTests
    {
        #region Fields & Init

        private Mock<IUnitOfWork> _uow;
        private Mock<IRepository<Game>> _gameRepository;
        private Mock<IRepository<Publisher>> _publisherRepository;
        private Mock<IRepository<Genre>> _genreRepository;
        private Mock<IRepository<PlatformType>> _platformRepository;
        private IGenreService _genreService;

        static GenreServiceTests()
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

            _genreService = new GenreService(_uow.Object);
        }

        #endregion

        #region Create

        [Test]
        public void Create_should_throw_ArgumentNullException_when_input_entity_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _genreService.Create(null));
        }

        [Test]
        public void Create_should_create_game()
        {
            //Arrange
            var genre = new GenreDTO() { Name = It.IsAny<string>()};

            //Act
            _genreService.Create(genre);

            //Assert
            _genreRepository.Verify(x => x.Create(It.IsAny<Genre>()));
        }
        #endregion

        #region Edit

        [Test]
        public void Edit_should_throw_ArgumentNullException_if_input_entity_is_null()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _genreService.Edit(null));
        }

        [Test]
        public void Edit_should_throw_ItemNotFoundException_if_genre_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            var genre = new GenreDTO() { Name = It.IsAny<string>() };
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Genre>(null);

            //Act & Assert
            Assert.Throws<ItemNotFoundException>(() => _genreService.Edit(genre));
        }

        [Test]
        public void Edit_should_edit_game()
        {
            //Arrange
            var genre = new GenreDTO() { Name = It.IsAny<string>() };
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Genre() { Name = It.IsAny<string>() });

            //Act
            _genreService.Edit(genre);

            //Assert
            _genreRepository.Verify(x => x.Update(It.IsAny<Genre>()));
        }

        #endregion

        #region Get

        [Test]
        public void Get_should_throw_ItemNotFoundException_if_genre_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Genre>(null);

            //Act & Assert
            Assert.Throws<ItemNotFoundException>(() => _genreService.Get(It.IsAny<int>()));
        }

        [Test]
        public void Get_should_get_genre_by_id()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Genre() { Name = It.IsAny<string>() });

            //Act
            var genre = _genreService.Get(It.IsAny<int>());

            //Assert
            _genreRepository.Verify(x => x.Get(It.IsAny<int>()));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_should_return_list_of_genres()
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAll()).Returns(new List<Genre>() as IOrderedQueryable<Genre>);

            //Act
            var genres = _genreService.GetAll();

            //Assert
            genres.Should().NotBeNull();
        }

        [Test]
        public void GetAll_should_return_empty_list_if_genres_do_not_exist()
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAll()).Returns<IEnumerable<Genre>>(null);

            //Act
            var genres = _genreService.GetAll();

            //Assert
            genres.Should().NotBeNull();
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_should_throw_ItemNotFoundException_if_genre_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Genre>(null);

            //Act & Assert
            Assert.Throws<ItemNotFoundException>(() => _genreService.Delete(It.IsAny<int>()));
        }

        [Test]
        public void Delete_should_delete_genre()
        {
            //Arrange
            _genreRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Genre() { Name = It.IsAny<string>() });

            //Act
            _genreService.Delete(It.IsAny<int>());

            //Assert
            _genreRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }

        #endregion
    }
}
