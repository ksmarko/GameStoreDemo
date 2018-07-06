using GameStore.BLL.Filtering.Factory;
using GameStore.BLL.Filtering.Parameters;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class PriceFilter : IFilter<IQueryable<Game>>
    {
        private readonly bool _descending;
        private readonly double _from;
        private readonly double _to;

        public PriceFilter(double from, double to, DirectionType direction)
        {
            _descending = direction == DirectionType.Descending;
            _from = from;
            _to = to;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            input = input.Where(x => x.Price >= _from && x.Price <= _to);

            return _descending ? input.OrderByDescending(x => x.Price) : input.OrderBy(x => x.Price);
        }
    }
}
