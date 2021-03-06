﻿using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformService
    {
        IEnumerable<PlatformTypeDTO> GetAll();
    }
}
