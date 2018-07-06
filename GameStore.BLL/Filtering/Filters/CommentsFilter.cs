using GameStore.BLL.Filtering.Factory;
using GameStore.BLL.Filtering.Parameters;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Filters
{
    class CommentsFilter : IFilter<IQueryable<Game>>
    {
        private readonly bool _descending;

        public CommentsFilter(DirectionType direction)
        {
            _descending = direction == DirectionType.Descending;
        }

        public IQueryable<Game> Execute(IQueryable<Game> input)
        {
            if (input == null || !input.Any())
            {
                return input;
            }

            return _descending ? input.OrderByDescending(x => x.Comments.Count) : input.OrderBy(x => x.Comments.Count);
        }
    }
}
