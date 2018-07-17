using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.BLL.DTO;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Filtering.Factory;
using GameStore.BLL.Filtering.Parameters;
using GameStore.BLL.Helpers;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork Database { get;}

        public GameService(IUnitOfWork uow)
        {
            Database = uow ?? throw new ArgumentNullException();
        }

        public void Create(GameDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var game = Mapper.Map<GameDTO, Game>(entity);
            game.Publisher = Database.Publishers.Find(x => x.Name == entity.Publisher).FirstOrDefault() ?? throw new PublisherNotFoundException();
            game.Genres = GetGenres(entity.Genres);
            game.PlatformTypes = GetPlatforms(entity.PlatformTypes);
            game.CreationDate = DateTime.Now;

            Database.Games.Create(game);
            Database.Save();
        }

        public void Edit(GameDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            var game = Database.Games.Get(entity.Id);

            if (game == null)
                throw new ItemNotFoundException();

            game.Name = entity.Name;
            game.Description = entity.Description;
            game.Price = entity.Price;
            
            Database.Games.Update(game);
            Database.Save();
        }

        private ICollection<Genre> GetGenres(IEnumerable<string> genresNames)
        {
            var genres = new List<Genre>();

            foreach (var el in genresNames)
                genres.Add(Database.Genres.Find(x => x.Name == el).FirstOrDefault() ?? throw new ItemNotFoundException("Genre not found"));

            return genres;
        }

        private ICollection<PlatformType> GetPlatforms(IEnumerable<string> platformsNames)
        {
            var platforms = new List<PlatformType>();

            foreach (var el in platformsNames)
                platforms.Add(Database.PlatformTypes.Find(x => x.Type == el).FirstOrDefault() ?? throw new ItemNotFoundException("Platform type not found"));

            return platforms;
        }

        public void Delete(int id)
        {
            var game = Database.Games.Get(id);

            if (game == null)
                throw new ItemNotFoundException();

            Database.Games.Delete(id);
            Database.Save();
        }

        public PagedList<GameDTO> GetAll(PaginationParameters paginationParameters, FilterParameters filterParameters)
        {
            if (paginationParameters == null || filterParameters == null)
                throw new ArgumentNullException();

            var games = Database.Games.GetAll();
            var pipeline = new PipelineFactory().Create(filterParameters);
            var filteredGames = pipeline.Process(games);

            return PagedList<GameDTO>.Create(filteredGames.ProjectTo<GameDTO>(), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public IEnumerable<GameDTO> GetByGenre(int genreId)
        {
            var genre = Database.Genres.Get(genreId);

            if (genre == null)
                throw new ItemNotFoundException();

            var query = Database.Games.Find(x => x.Genres.Select(g => g.Id).Contains(genreId));
            return Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(query);
        }

        public IEnumerable<GameDTO> GetByPlatformType(int platformId)
        {
            var platformType = Database.PlatformTypes.Get(platformId);

            if (platformType == null)
                throw new ItemNotFoundException();

            var query = Database.Games.Find(x => x.PlatformTypes.Select(g => g.Id).Contains(platformId));
            return Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(query);
        }

        public GameDTO Get(int id)
        {
            var game = Database.Games.Get(id);

            if (game == null)
                throw new ItemNotFoundException();

            game.Views++;
            Database.Games.Update(game);

            return Mapper.Map<Game, GameDTO>(game);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
