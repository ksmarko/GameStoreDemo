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
    public class GenreService : IGenreService
    {
        private IUnitOfWork Database { get; }

        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(GenreDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var genre = Mapper.Map<GenreDTO, Genre>(entity);

            Database.Genres.Create(genre);
            Database.Save();
        }

        public void Edit(GenreDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var genre = Database.Genres.Get(entity.Id);

            if (genre == null)
                throw new ItemNotFoundException();

            genre.Name = entity.Name;

            Database.Genres.Update(genre);
            Database.Save();
        }

        public void Delete(int id)
        {
            var genre = Database.Genres.Get(id);

            if (genre == null)
                throw new ItemNotFoundException();

            //TODO: replace to the context file
            foreach (var el in Database.Genres.Find(x => x.ParentId == id).ToList())
                Database.Genres.Delete(el.Id);

            Database.Genres.Delete(id);
            Database.Save();
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(Database.Genres.GetAll());
        }

        public GenreDTO Get(int id)
        {
            var genre = Database.Genres.Get(id);

            if (genre == null)
                throw new ItemNotFoundException();

            return Mapper.Map<Genre, GenreDTO>(genre);
        }
    }
}
