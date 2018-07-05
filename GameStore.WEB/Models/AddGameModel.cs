using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models
{
    public class AddGameModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Genres field is required")]
        public string[] Genres { get; set; }

        [Required(ErrorMessage = "Platforms field is required")]
        public string[] PlatformTypes { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }
        
        [Required(ErrorMessage = "Please input price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please input publication date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
    }
}