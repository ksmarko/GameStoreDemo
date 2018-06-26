using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public Publisher()
        {
            Games = new List<Game>();
        }
    }
}
