using System;
using System.Linq;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Filtering.Parameters;

namespace GameStore.BLL.Filtering.Factory
{
    class PipelineFactory
    {
        private GameSelectionPipeline _pipeline = new GameSelectionPipeline();

        public GameSelectionPipeline Create(FilterParameters parameters)
        {
            _pipeline.Register(new DefaultFilter());

            if (parameters.Genres.Any())
                _pipeline.Register(new GenresFilter(parameters.Genres));

            if (parameters.Platforms.Any())
                _pipeline.Register(new PlatformsFilter(parameters.Platforms));

            if (parameters.Publishers.Any())
                _pipeline.Register(new PublishersFilter(parameters.Publishers));

            if (parameters.Views) 
                _pipeline.Register(new ViewsFilter(parameters.Direction));

            if (parameters.Comments)
                _pipeline.Register(new CommentsFilter(parameters.Direction));

            if (parameters.Date)
                _pipeline.Register(new DateFilter(parameters.Direction));

            if (parameters.Price)
                _pipeline.Register(new PriceFilter(parameters.PriceFrom, parameters.PriceTo, parameters.Direction));

            if (!String.IsNullOrEmpty(parameters.Name))
                _pipeline.Register(new NameFilter(parameters.Name));

            //TODO: Add filter by date: last week/last month/last year/2 year/3year. Radio-button group. *by date means when game was published.

            return _pipeline;
        }
    }
}
