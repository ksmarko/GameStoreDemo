using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService : IDisposable
    {
        void AddComment(int gameId, CommentDTO entity);
        void Reply(int commentId, CommentDTO entity);
        IEnumerable<CommentDTO> GetAll(int gameId);
    }
}
