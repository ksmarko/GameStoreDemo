using GameStore.BLL.Helpers;
using System.Web.Http;
using System.Web.Http.Routing;

namespace GameStore.WEB.Helpers
{
    public class PaginationHelper
    {
        public static PaginationMetadata GetPaginationMetadata<T>(PagedList<T> source,
            PaginationParameters paginationParameters, ApiController controller, string routeName)
        {
            var urlHelper = new UrlHelper(controller.Request);

            var previousPageLink = source.HasPrevious ? urlHelper.Link(routeName, new
            {
                pageNumber = paginationParameters.PageNumber - 1,
                pageSize = paginationParameters.PageSize
            }) : null;

            var nextPageLink = source.HasNext ? urlHelper.Link(routeName, new
            {
                pageNumber = paginationParameters.PageNumber + 1,
                pageSize = paginationParameters.PageSize
            }) : null;

            var paginationMetadata = new PaginationMetadata(source.TotalCount, source.PageSize, source.CurrentPage, source.TotalPages, previousPageLink, nextPageLink);

            return paginationMetadata;
        }
    }
}