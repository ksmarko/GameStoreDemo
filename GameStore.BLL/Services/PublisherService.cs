using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Exceptions;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private IUnitOfWork Database { get; }

        public PublisherService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(PublisherDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var publisher = Mapper.Map<PublisherDTO, Publisher>(entity);

            Database.Publishers.Create(publisher);
            Database.Save();
        }

        public void Delete(int id)
        {
            var publisher = Database.Publishers.Get(id);

            if (publisher == null)
                throw new PublisherNotFoundException();

            //TODO: replace to the context file
            foreach (var el in Database.Games.Find(x => x.Publisher.Id == id).ToList())
                Database.Games.Delete(el.Id);

            Database.Publishers.Delete(id);
            Database.Save();
        }

        public void Edit(PublisherDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var publisher = Database.Publishers.Get(entity.Id);

            if (publisher == null)
                throw new PublisherNotFoundException();

            publisher.Name = entity.Name;

            Database.Publishers.Update(publisher);
            Database.Save();
        }

        public PublisherDTO Get(int id)
        {
            var publisher = Database.Publishers.Get(id);

            if (publisher == null)
                throw new PublisherNotFoundException();

            return Mapper.Map<Publisher, PublisherDTO>(publisher);
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDTO>>(Database.Publishers.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
