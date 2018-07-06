using GameStore.BLL.DTO;
using GameStore.BLL.Filtering.Parameters;
using GameStore.BLL.Helpers;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IService<GameDTO>
    {
        IEnumerable<GameDTO> GetByGenre(int genreId);
        IEnumerable<GameDTO> GetByPlatformType(int platformId);
        PagedList<GameDTO> GetAll(PaginationParameters paginationParameters, FilterParameters filterParameters);
    }
}
