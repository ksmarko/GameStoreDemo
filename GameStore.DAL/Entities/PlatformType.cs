using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class PlatformType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public PlatformType()
        {
            Games = new List<Game>();
        }
    }
}
