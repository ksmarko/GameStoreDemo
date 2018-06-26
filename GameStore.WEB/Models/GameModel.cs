using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string[] Genres { get; set; }

        [Required]
        public string[] PlatformTypes { get; set; }

        [Required]
        public string Publisher { get; set; }
    }
}