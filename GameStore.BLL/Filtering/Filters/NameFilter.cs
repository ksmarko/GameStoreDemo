using GameStore.BLL.Filtering.Factory;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class NameFilter : IFilter<IQueryable<Game>>
    {
        private readonly string _name;

        public NameFilter(string name)
        {
            _name = name;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }
            return input.Where(x => x.Name.Contains(_name)).OrderBy(x => x.Name);
        }
    }
}
