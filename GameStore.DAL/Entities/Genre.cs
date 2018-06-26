﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Genre> Subgenres { get; set; }
        public int? ParentId { get; set; }
        public virtual Genre Parent { get; set; }

        public Genre()
        {
            Games = new List<Game>();
        }
    }
}
