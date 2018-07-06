using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Filtering.Factory
{
    class GameSelectionPipeline : Pipeline<IQueryable<Game>>
    {
        public override IQueryable<Game> Process(IQueryable<Game> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }
}
