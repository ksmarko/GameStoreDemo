using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService : IService<PublisherDTO>
    {
        IEnumerable<PublisherDTO> GetAll();
    }
}
