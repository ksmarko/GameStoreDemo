using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPlatformService : IDisposable
    {
        IEnumerable<PlatformTypeDTO> GetAll();
    }
}
