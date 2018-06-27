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
    public class PublisherControllerTests
    {
        #region Fields & Init

        private Mock<IPublisherService> _publisherService;
        private PublisherController _publisherController;

        static PublisherControllerTests()
        {
            Configuration.AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _publisherService = new Mock<IPublisherService>();

            _publisherController = new PublisherController(_publisherService.Object);
        }

        #endregion

        [Test]
        public void PublisherController_should_throw_ArgumentNullException_if_input_service_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _publisherController = new PublisherController(null));
        }

        [Test]
        public void CreatePublisher_should_create_publisher()
        {
            //Act
            var actionResult = _publisherController.CreatePublisher(new PublisherModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void Get_should_return_publisher_by_id()
        {
            //Arrange
            int id = It.IsAny<int>();
            _publisherService.Setup(x => x.Get(It.IsAny<int>())).Returns(new PublisherDTO() { Id = id });

            //Act
            var actionResult = _publisherController.GetPublisher(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<PublisherModel>();
            Assert.AreEqual(id, actionResult.Id);
        }

        [Test]
        public void EditPublisher_should_edit_publisher_data()
        {
            //Act
            var actionResult = _publisherController.EditPublisher(It.IsAny<int>(), new PublisherModel());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void DeletePublisher_should_delete_publisher()
        {
            //Act
            var actionResult = _publisherController.DeletePublisher(It.IsAny<int>());

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test] public void GetPublishers_should_return_all_publishers()
        {
            //Arrange
            _publisherService.Setup(x => x.GetAll()).Returns(new List<PublisherDTO>() { new PublisherDTO() });

            //Act
            var actionResult = _publisherController.GetPublishers();

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }

        [Test]
        public void GetPublisherGames_should_return_list_of_games_for_publisher()
        {
            //Arrange
            _publisherService.Setup(x => x.Get(It.IsAny<int>())).Returns(new PublisherDTO() { Games = new List<GameDTO>() { It.IsAny<GameDTO>() } });

            //Act
            var actionResult = _publisherController.GetPublisherGames(It.IsAny<int>());

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }
    }
}
