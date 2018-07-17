using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork Database { get;}

        public CommentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddComment(int gameId, CommentDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var game = Database.Games.Get(gameId);
            var comment = Mapper.Map<CommentDTO, Comment>(entity);
            comment.Game = game ?? throw new ItemNotFoundException();
            comment.Publisher = Database.Publishers.Find(x => x.Name == entity.Publisher).FirstOrDefault() ?? throw new PublisherNotFoundException();

            Database.Comments.Create(comment);
            Database.Save();
        }

        public void Reply(int commentId, CommentDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var parentComment = Database.Comments.Get(commentId);

            if (parentComment == null)
                throw new ItemNotFoundException();

            var comment = Mapper.Map<CommentDTO, Comment>(entity);
            comment.Game = parentComment.Game;
            comment.Parent = parentComment;
            comment.Body = parentComment.Publisher.Name + ", " + entity.Body;
            comment.Publisher = Database.Publishers.Find(x => x.Name == entity.Publisher).FirstOrDefault() ?? throw new PublisherNotFoundException();

            Database.Comments.Create(comment);
            Database.Save();
        }

        public IEnumerable<CommentDTO> GetAll(int gameId)
        {
            var game = Database.Games.Get(gameId);

            if (game == null)
                throw new ItemNotFoundException();

            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(Database.Comments.Find(x => x.Game.Id == game.Id));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
