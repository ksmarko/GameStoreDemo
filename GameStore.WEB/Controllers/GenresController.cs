using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GameStore.WEB.Controllers
{
    public class GenresController : ApiController
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult CreateGenre(GenreModel model)
        {
            var genre = Mapper.Map<GenreModel, GenreDTO>(model);
            _genreService.Create(genre);

            return Ok("Genre created");
        }

        [HttpGet]
        [Route("api/genres/{id}")]
        public GenreModel GetGenre(int id)
        {
            return Mapper.Map<GenreDTO, GenreModel>(_genreService.Get(id));
        }

        [HttpGet]
        public IEnumerable<GenreModel> GetGenres()
        {
            return Mapper.Map<IEnumerable<GenreDTO>, IEnumerable<GenreModel>>(_genreService.GetAll());
        }

        [HttpPut]
        [Route("api/genres/{id}")]
        public IHttpActionResult EditGenre(int id, GenreModel model)
        {
            var genre = Mapper.Map<GenreModel, GenreDTO>(model);
            genre.Id = id;
            _genreService.Edit(genre);

            return Ok("Genre edited");
        }

        [HttpDelete]
        [Route("api/genres/{id}")]
        public IHttpActionResult DeleteGenre(int id) 
        {
            _genreService.Delete(id);

            return Ok("Genre deleted");
        }

        [HttpGet]
        [Route("api/genres/{id}/games")]
        public IEnumerable<GameModel> GetGames(int id)
        {
            var genre = _genreService.Get(id);

            return Mapper.Map<IEnumerable<GameDTO>, IEnumerable<GameModel>>(genre.Games);
        }
    }
}
