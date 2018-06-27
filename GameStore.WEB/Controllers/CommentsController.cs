using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GameStore.WEB.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("api/games/{gameId}/comments")]
        public IHttpActionResult AddComment(int gameId, AddCommentModel model)
        {
            var comment = Mapper.Map<AddCommentModel, CommentDTO>(model);

            _commentService.AddComment(gameId, comment);

            return Ok("Comment added");
        }

        [HttpPost]
        [Route("api/comments/{commentId}/comments")]
        public IHttpActionResult Reply(int commentId, AddCommentModel model)
        { 
            var comment = Mapper.Map<AddCommentModel, CommentDTO>(model);

            _commentService.Reply(commentId, comment);

            return Ok("Reply added");
        }

        [HttpGet]
        [Route("api/games/{id}/comments")]
        public IEnumerable<CommentModel> GetComments(int id)
        {
            var comments = Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<CommentModel>>(_commentService.GetAll(id));

            return comments;
        }
    }
}
