using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;

namespace GameStore.WEB.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("api/games/{gameId}/comments")]
        public IHttpActionResult AddComment(int gameId, AddCommentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var comment = Mapper.Map<AddCommentModel, CommentDTO>(model);

            _commentService.AddComment(gameId, comment);

            return Ok("Comment added");
        }

        [HttpPost]
        [Route("api/comments/{commentId}/comments")]
        public IHttpActionResult Reply(int commentId, AddCommentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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
