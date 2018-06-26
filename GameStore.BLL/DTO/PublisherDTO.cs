using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameDTO> Games { get; set; }

        public PublisherDTO()
        {
            Games = new List<GameDTO>();
        }
    }
}
