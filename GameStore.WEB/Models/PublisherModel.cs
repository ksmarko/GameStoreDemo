using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models
{
    public class PublisherModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Max length is 255")]
        public string Name { get; set; }
    }
}