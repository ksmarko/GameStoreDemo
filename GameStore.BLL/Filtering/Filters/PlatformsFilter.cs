using GameStore.BLL.Filtering.Factory;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class PlatformsFilter : IFilter<IQueryable<Game>>
    {
        private IEnumerable<string> _platforms;

        public PlatformsFilter(IEnumerable<string> platforms)
        {
            _platforms = platforms;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return input.Where(x => x.PlatformTypes.Select(c => c.Type).Intersect(_platforms).Count() != 0);
        }
    }
}
