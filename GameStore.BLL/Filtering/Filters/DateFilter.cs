using GameStore.BLL.Filtering.Factory;
using GameStore.BLL.Filtering.Parameters;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class DateFilter : IFilter<IQueryable<Game>>
    {
        private readonly bool _descending;

        public DateFilter(DirectionType direction)
        {
            _descending = direction == DirectionType.Descending;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return _descending ? input.OrderByDescending(x => x.CreationDate) : input.OrderBy(x => x.CreationDate);
        }
    }
}
