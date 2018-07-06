using GameStore.BLL.Filtering.Factory;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class DefaultFilter : IFilter<IQueryable<Game>>
    {
        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }
            return input.OrderBy(x => x.Name);
        }
    }
}
