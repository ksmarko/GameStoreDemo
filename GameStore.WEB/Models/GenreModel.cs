using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Parent { get; set; }
    }
}