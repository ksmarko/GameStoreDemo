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
    public class PlatformServiceTest
    {
        #region Fields & Init

        private Mock<IUnitOfWork> _uow;
        private Mock<IRepository<PlatformType>> _platformRepository;
        private IPlatformService _platformService;

        static PlatformServiceTest()
        {
            AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _uow = new Mock<IUnitOfWork>();

            _platformRepository = new Mock<IRepository<PlatformType>>();
            _uow.Setup(x => x.PlatformTypes).Returns(_platformRepository.Object);

            _platformService = new PlatformService(_uow.Object);
        }

        #endregion

        #region PlatformService

        [Test]
        public void PlatformService_should_throw_ArgumentNullException_when_unit_of_work_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _platformService = new PlatformService(null));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_should_return_list_of_platforms()
        {
            //Arrange
            _platformRepository.Setup(x => x.GetAll()).Returns(new List<PlatformType>());

            //Act
            var platforms = _platformService.GetAll();

            //Assert
            platforms.Should().NotBeNull();
        }

        [Test]
        public void GetAll_should_return_empty_list_if_platforms_do_not_exist()
        {
            //Arrange
            _platformRepository.Setup(x => x.GetAll()).Returns<IEnumerable<PlatformType>>(null);

            //Act
            var platforms = _platformService.GetAll();

            //Assert
            platforms.Should().NotBeNull();
        }

        #endregion
    }
}