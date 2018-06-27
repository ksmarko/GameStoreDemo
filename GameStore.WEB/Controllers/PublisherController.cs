using System;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace GameStore.WEB.Controllers
{
    public class PublisherController : ApiController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult CreatePublisher(PublisherModel model)
        {
            var publisher = Mapper.Map<PublisherModel, PublisherDTO>(model);
            _publisherService.Create(publisher);

            return Ok("Publisher created");
        }

        [HttpGet]
        [Route("api/publisher/{id}")]
        public PublisherModel GetPublisher(int id)
        {
            return Mapper.Map<PublisherDTO, PublisherModel>(_publisherService.Get(id));
        }

        [HttpGet]
        public IEnumerable<PublisherModel> GetPublishers()
        {
            return Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<PublisherModel>>(_publisherService.GetAll());
        }

        [HttpPut]
        [Route("api/publisher/{id}")]
        public IHttpActionResult EditPublisher(int id, PublisherModel model)
        {
            var publisher = Mapper.Map<PublisherModel, PublisherDTO>(model);
            publisher.Id = id;
            _publisherService.Edit(publisher);

            return Ok("Publisher edited");
        }

        [HttpDelete]
        [Route("api/publisher/{id}")]
        public IHttpActionResult DeletePublisher(int id)
        {
            _publisherService.Delete(id);

            return Ok("Publisher deleted");
        }

        [HttpGet]
        [Route("api/publisher/{id}/games")]
        public IEnumerable<GameModel> GetPublisherGames(int id)
        {
            var publisher = _publisherService.Get(id);

            return Mapper.Map<IEnumerable<GameDTO>, IEnumerable<GameModel>>(publisher.Games);
        }
    }
}
