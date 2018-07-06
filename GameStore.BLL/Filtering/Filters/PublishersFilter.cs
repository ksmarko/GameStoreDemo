using GameStore.BLL.Filtering.Factory;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class PublishersFilter : IFilter<IQueryable<Game>>
    {
        private IEnumerable<string> _publishers;

        public PublishersFilter(IEnumerable<string> publishers)
        {
            _publishers = publishers;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return input.Where(x => _publishers.Contains(x.Publisher.Name));
        }
    }
}
