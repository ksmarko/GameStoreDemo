using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService : IDisposable
    {
        void AddComment(int gameId, CommentDTO entity);
        void Reply(int commentId, CommentDTO entity);
        IEnumerable<CommentDTO> GetAll(int gameId);
    }
}
