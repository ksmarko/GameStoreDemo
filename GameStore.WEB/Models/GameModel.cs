namespace GameStore.WEB.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Genres { get; set; }

        public string[] PlatformTypes { get; set; }

        public string Publisher { get; set; }

        public int Views { get; set; }

        public double Price { get; set; }

        public string CreationDate { get; set; }

        public string PublicationDate { get; set; }
    }
}