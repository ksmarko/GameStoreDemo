using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int Views { get; set; }
        public double Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<string> Genres { get; set; }
        public ICollection<string> PlatformTypes { get; set; }

        public GameDTO()
        {
            Comments = new List<CommentDTO>();
            Genres = new List<string>();
            PlatformTypes = new List<string>();
        }
    }
}
