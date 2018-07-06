using GameStore.BLL.Filtering.Factory;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class GenresFilter : IFilter<IQueryable<Game>>
    {
        private IEnumerable<string> _genres;

        public GenresFilter(IEnumerable<string> genres)
        {
            _genres = genres;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return input.Where(x => x.Genres.Select(c => c.Name).Intersect(_genres).Count() != 0);
        }
    }
}
