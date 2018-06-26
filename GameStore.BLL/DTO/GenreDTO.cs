using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameDTO> Games { get; set; }
        public ICollection<GenreDTO> Subgenres { get; set; }
        public GenreDTO Parent { get; set; }

        public GenreDTO()
        {
            Games = new List<GameDTO>();
            Subgenres = new List<GenreDTO>();
        }
    }
}
