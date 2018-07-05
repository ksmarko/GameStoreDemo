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

namespace GameStore.BLL.Tests.Services
{
    [TestFixture]
    public class PublisherServiceTests
    {
        #region Fields & Init

        private Mock<IUnitOfWork> _uow;
        private Mock<IRepository<Game>> _gameRepository;
        private Mock<IRepository<Publisher>> _publisherRepository;
        private Mock<IRepository<Genre>> _genreRepository;
        private Mock<IRepository<PlatformType>> _platformRepository;
        private IPublisherService _publisherService;

        static PublisherServiceTests()
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

            _publisherService = new PublisherService(_uow.Object);
        }

        #endregion

        #region GameService

        [Test]
        public void GameService_should_throw_ArgumentNullException_when_unit_of_work_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService = new PublisherService(null));
        }

        #endregion

        #region Create

        [Test]
        public void Create_should_throw_ArgumentNullException_when_input_entity_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService.Create(null));
        }

        [Test]
        public void Create_should_create_publisher()
        {
            //Arrange
            var publisher = new PublisherDTO() { Name = It.IsAny<string>() };

            //Act
            _publisherService.Create(publisher);

            //Assert
            _publisherRepository.Verify(x => x.Create(It.IsAny<Publisher>()));
        }
        #endregion

        #region Edit

        [Test]
        public void Edit_should_throw_ArgumentNullException_if_input_entity_is_null()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService.Edit(null));
        }

        [Test]
        public void Edit_should_throw_ArgumentNullException_if_publisher_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            var publisher = new PublisherDTO() { Name = It.IsAny<string>() };
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Publisher>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService.Edit(publisher));
        }

        [Test]
        public void Edit_should_edit_publisher_data()
        {
            //Arrange
            var publisher = new PublisherDTO() { Name = It.IsAny<string>() };
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Publisher() { Name = It.IsAny<string>() });

            //Act
            _publisherService.Edit(publisher);

            //Assert
            _publisherRepository.Verify(x => x.Update(It.IsAny<Publisher>()));
        }

        #endregion

        #region Get

        [Test]
        public void Get_should_throw_ArgumentNullException_if_publisher_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Publisher>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService.Get(It.IsAny<int>()));
        }

        [Test]
        public void Get_should_get_publisher_by_id()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Publisher() { Name = It.IsAny<string>() });

            //Act
            var publisher = _publisherService.Get(It.IsAny<int>());

            //Assert
            _publisherRepository.Verify(x => x.Get(It.IsAny<int>()));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_should_return_list_of_publishers()
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAll()).Returns(new List<Publisher>() as IOrderedQueryable<Publisher>);

            //Act
            var publishers = _publisherService.GetAll();

            //Assert
            publishers.Should().NotBeNull();
        }

        [Test]
        public void GetAll_should_return_empty_list_if_publishers_do_not_exist()
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAll()).Returns<IEnumerable<Publisher>>(null);

            //Act
            var publishers = _publisherService.GetAll();

            //Assert
            publishers.Should().NotBeNull();
        }

        #endregion

        #region Delete

        [Test]
        public void Delete_should_throw_ArgumentNullException_if_publisher_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Publisher>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherService.Delete(It.IsAny<int>()));
        }

        [Test]
        public void Delete_should_delete_publisher()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Publisher() { Name = It.IsAny<string>() });

            //Act
            _publisherService.Delete(It.IsAny<int>());

            //Assert
            _publisherRepository.Verify(x => x.Delete(It.IsAny<int>()));
        }

        #endregion
    }
}
