using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService : IService<GenreDTO>
    {
        IEnumerable<GenreDTO> GetAll();
    }
}
