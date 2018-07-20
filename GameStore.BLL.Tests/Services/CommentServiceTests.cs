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
    public class CommentServiceTests
    {
        #region Fields & Init

        private Mock<IUnitOfWork> _uow;
        private Mock<IRepository<Game>> _gameRepository;
        private Mock<IRepository<Publisher>> _publisherRepository;
        private Mock<IRepository<Genre>> _genreRepository;
        private Mock<IRepository<PlatformType>> _platformRepository;
        private Mock<IRepository<Comment>> _commentRepository;
        private ICommentService _commentService;

        static CommentServiceTests()
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
            _commentRepository = new Mock<IRepository<Comment>>();

            _uow.Setup(x => x.Games).Returns(_gameRepository.Object);
            _uow.Setup(x => x.Publishers).Returns(_publisherRepository.Object);
            _uow.Setup(x => x.Genres).Returns(_genreRepository.Object);
            _uow.Setup(x => x.PlatformTypes).Returns(_platformRepository.Object);
            _uow.Setup(x => x.Comments).Returns(_commentRepository.Object);

            _commentService = new CommentService(_uow.Object);
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_should_throw_ItemNotFoundException_if_game_with_specified_id_does_not_exist_in_the_database()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Game>(null);

            //Act & Assert
            Assert.Throws<ItemNotFoundException>(() => _commentService.GetAll(It.IsAny<int>()));
        }

        [Test]
        public void GetAll_should_return_list_of_comments()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game());
            _commentRepository.Setup(x => x.Find(It.IsAny<Func<Comment, bool>>())).Returns(new List<Comment>());

            //Act
            var comments = _commentService.GetAll(It.IsAny<int>());

            //Assert
            comments.Should().NotBeNull();
        }

        [Test]
        public void GetAll_should_return_empty_list_if_comments_do_not_exist()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game());
            _commentRepository.Setup(x => x.Find(It.IsAny<Func<Comment, bool>>())).Returns<IEnumerable<Comment>>(null);
            //Act
            var games = _commentService.GetAll(It.IsAny<int>());

            //Assert
            games.Should().NotBeNull();
        }

        #endregion

        #region Reply

        [Test]
        public void Reply_should_throw_ArgumentNullException_if_input_comment_is_null()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commentService.Reply(It.IsAny<int>(), null));
        }

        [Test]
        public void Reply_should_throw_ArgumentNullException_if_parent_comment_is_null()
        {
            //Arrange
            _commentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Comment>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commentService.Reply(It.IsAny<int>(), It.IsAny<CommentDTO>()));
        }

        [Test]
        public void Reply_should_reply_if_publisher_exists_in_the_database()
        {
            //Arrange
            var comment = new CommentDTO() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>() };
            _commentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Comment() {Publisher = new Publisher() {Name = It.IsAny<string>()}});
            _publisherRepository.Setup(x => x.Find(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>() { new Publisher() { Name = It.IsAny<string>() } });

            //Act
            _commentService.Reply(It.IsAny<int>(), comment);

            //Assert
            _commentRepository.Verify(x => x.Create(It.IsAny<Comment>()));
        }

        //[Test]
        //public void Create_should_throw_PublisherNotFoundException_if_publisher_does_not_exist_in_the_database()
        //{
        //    //Arrange
        //    var comment = new CommentDTO() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>() };
        //    _commentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Comment() { Publisher = new Publisher() { Name = It.IsAny<string>() } });
        //    _publisherRepository.Setup(x => x.Find(It.IsAny<Func<Publisher, bool>>())).Returns<IEnumerable<Publisher>>(null);

        //    //Assert
        //    Assert.Throws<PublisherNotFoundException>(() => _commentService.Reply(It.IsAny<int>(), comment));
        //}

        #endregion

        #region AddComment

        [Test]
        public void AddComment_should_throw_ArgumentNullException_if_input_comment_is_null()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commentService.AddComment(It.IsAny<int>(), null));
        }

        [Test]
        public void AddComment_should_throw_ArgumentNullException_if_game_does_not_exist()
        {
            //Arrange
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<Game>(null);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _commentService.AddComment(It.IsAny<int>(), It.IsAny<CommentDTO>()));
        }

        [Test]
        public void AddComment_should_add_if_publisher_exists_in_the_database()
        {
            //Arrange
            var comment = new CommentDTO() { Name = It.IsAny<string>(), Publisher = It.IsAny<string>() };
            _gameRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Game());
            _publisherRepository.Setup(x => x.Find(It.IsAny<Func<Publisher, bool>>())).Returns(new List<Publisher>() { new Publisher() { Name = It.IsAny<string>() } });

            //Act
            _commentService.AddComment(It.IsAny<int>(), comment);

            //Assert
            _commentRepository.Verify(x => x.Create(It.IsAny<Comment>()));
        }

        #endregion
    }
}
