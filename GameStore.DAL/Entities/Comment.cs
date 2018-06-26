using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
