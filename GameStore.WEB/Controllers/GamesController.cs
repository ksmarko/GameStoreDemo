using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Helpers;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Helpers;
using GameStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace GameStore.WEB.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [Route("api/games")]
        public IHttpActionResult CreateGame(AddGameModel model)
        {
            var game = Mapper.Map<AddGameModel, GameDTO>(model);
            _gameService.Create(game);

            return Ok("Game created");
        }

        [HttpGet]
        [Route("api/games/{id}")]
        public GameModel GetGame(int id)
        {
            return Mapper.Map<GameDTO, GameModel>(_gameService.Get(id));
        }

        [HttpGet]
        [Route("api/games", Name = "GetGames")]
        public IHttpActionResult GetGames([FromUri] PaginationParameters paginationParameters)
        {
            var parameters = paginationParameters ?? new PaginationParameters();
            var games = _gameService.GetAll(parameters);
            var paginationMetadata = PaginationHelper.GetPaginationMetadata(games, parameters, this, "GetGames");
            var items = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<GameModel>>(games);
            var response = Request.CreateResponse(HttpStatusCode.OK, items);
            response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return ResponseMessage(response);
        }

        [HttpPut]
        [Route("api/games/{id}")]
        public IHttpActionResult EditGame(int id, EditGameModel model)
        {
            var game = Mapper.Map<EditGameModel, GameDTO>(model);
            game.Id = id;
            _gameService.Edit(game);

            return Ok("Game edited");
        }

        [HttpDelete]
        [Route("api/games/{id}")]
        public IHttpActionResult DeleteGame(int id)
        {
            _gameService.Delete(id);

            return Ok("Game deleted");
        }

        [HttpGet]
        [Route("api/games/{id}/download")]
        public HttpResponseMessage DownloadGame(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/file.bin", FileMode.Open);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileStream.Name;
            return response;
        }

        [HttpGet]
        [Route("api/games/{id}/genres")]
        public IEnumerable<string> GetGenresForGame(int id)
        {
            var game = _gameService.Get(id);

            return game.Genres;
        }
    }
}
