﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IService<GameDTO>
    {
        IEnumerable<GameDTO> GetByGenre(int genreId);
        IEnumerable<GameDTO> GetByPlatformType(int platformId);
    }
}
