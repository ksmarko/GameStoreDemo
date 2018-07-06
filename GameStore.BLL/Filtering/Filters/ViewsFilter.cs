using GameStore.DAL.Entities;
using System.Linq;
using GameStore.BLL.Filtering.Factory;
using GameStore.BLL.Filtering.Parameters;

namespace GameStore.BLL.Filtering.Filters
{
    class ViewsFilter : IFilter<IQueryable<Game>>
    {
        private readonly bool _descending;

        public ViewsFilter(DirectionType direction)
        {
            _descending = direction == DirectionType.Descending;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return _descending ? input.OrderByDescending(x => x.Views) : input.OrderBy(x => x.Views);
        }
    }
}
