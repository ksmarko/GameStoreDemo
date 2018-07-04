using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<GameModel> Games { get; set; }
    }
}