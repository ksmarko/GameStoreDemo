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
    public class CommentsControllerTests
    {
        #region Fields & Init

        private Mock<ICommentService> _commentService;
        private CommentsController _commentController;

        static CommentsControllerTests()
        {
            GameStore.WEB.Tests.Configuration.AutoMapperInitializer.Initialize();
        }

        [SetUp]
        public void Initialize()
        {
            _commentService = new Mock<ICommentService>();

            _commentController = new CommentsController(_commentService.Object);
        }

        #endregion

        [Test]
        public void CommentController_should_throw_ArgumentNullException_if_input_service_is_null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commentController = new CommentsController(null));
        }

        [Test]
        public void AddComment_should_add_comment()
        {
            //Act
            var actionResult = _commentController.AddComment(It.IsAny<int>(), new AddCommentModel() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>(), Body = It.IsAny<string>() });

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void Reply_should_add_comment()
        {
            //Act
            var actionResult = _commentController.Reply(It.IsAny<int>(), new AddCommentModel() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>(), Body = It.IsAny<string>() });

            //Assert
            actionResult.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void GetAll_should_return_all_comments()
        {
            //Arrange
            _commentService.Setup(x => x.GetAll(It.IsAny<int>())).Returns(new List<CommentDTO>() {new CommentDTO()});

            //Act
            var actionResult = _commentController.GetComments(It.IsAny<int>());

            //Assert
            Assert.AreEqual(1, actionResult.Count());
        }
    }
}
