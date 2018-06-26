using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork Database { get;}

        public CommentService(IUnitOfWork uow)
        {
            Database = uow ?? throw new ArgumentNullException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void AddComment(int gameId, CommentDTO entity)
        {
            var game = Database.Games.Get(gameId);

            if (game == null || entity == null)
                throw new ArgumentNullException();

            var comment = Mapper.Map<CommentDTO, Comment>(entity);
            comment.Game = game;
            comment.Publisher = Database.Publishers.Find(x => x.Name == entity.Publisher).FirstOrDefault() ?? new Publisher() { Name = entity.Publisher };

            Database.Comments.Create(comment);
            Database.Save();
        }

        public void Reply(int commentId, CommentDTO entity)
        {
            var parentComment = Database.Comments.Get(commentId);

            if (parentComment == null || entity == null)
                throw new ArgumentNullException();

            var comment = Mapper.Map<CommentDTO, Comment>(entity);
            comment.Game = parentComment.Game;
            comment.Parent = parentComment;
            comment.Body = parentComment.Publisher.Name + ", " + entity.Body;
            comment.Publisher = Database.Publishers.Find(x => x.Name == entity.Publisher).FirstOrDefault() ?? new Publisher() { Name = entity.Publisher };

            Database.Comments.Create(comment);
            Database.Save();
        }

        public IEnumerable<CommentDTO> GetAll(int gameId)
        {
            var game = Database.Games.Get(gameId);

            if (game == null)
                throw new ArgumentNullException();

            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(Database.Comments.Find(x => x.Game.Id == game.Id));
        }
    }
}
