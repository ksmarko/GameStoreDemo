using GameStore.BLL.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace GameStore.WEB.Helpers
{
    public class PaginationHelper
    {
        //INFO: Maybe exists best way?

        //gets current uri and use its query for creating next and previous pages links
        public static PaginationMetadata GetPaginationMetadata<T>(PagedList<T> source, 
            PaginationParameters paginationParameters, ApiController controller)
        {
            var uri = controller.Request.RequestUri.AbsoluteUri;

            //removes old pagination data
            Regex r1 = new Regex($"&?{nameof(paginationParameters.PageSize)}=\\d+\\??", RegexOptions.IgnoreCase);
            Regex r2 = new Regex($"&?{nameof(paginationParameters.PageNumber)}=\\d+\\??", RegexOptions.IgnoreCase);
            uri = r1.Replace(uri, String.Empty);
            uri = r2.Replace(uri, String.Empty);


            //add new pagination data
            var previousPageLink = source.HasPrevious ? 
                uri + $"&{nameof(paginationParameters.PageNumber)}={paginationParameters.PageNumber - 1}&{nameof(paginationParameters.PageSize)}={paginationParameters.PageSize}" 
                : null;

            var nextPageLink = source.HasNext ? 
                uri + $"&{nameof(paginationParameters.PageNumber)}={paginationParameters.PageNumber + 1}&{nameof(paginationParameters.PageSize)}={paginationParameters.PageSize}" 
                : null;

            var paginationMetadata = new PaginationMetadata(source.TotalCount, source.PageSize, source.CurrentPage, source.TotalPages, previousPageLink, nextPageLink);

            return paginationMetadata;
        }
    }
}