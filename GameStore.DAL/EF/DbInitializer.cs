using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var genres = new List<Genre>()
            {
                new Genre() {Id = 1, Name = "Strategy"},
                new Genre() {Id = 2, Name = "RPG"},
                new Genre() {Id = 3, Name = "Sports"},
                new Genre() {Id = 4, Name = "Races"},
                new Genre() {Id = 5, Name = "Action"},
                new Genre() {Id = 6, Name = "Adventure"},
                new Genre() {Id = 7, Name = "Puzzle&Skill"},
                new Genre() {Id = 8, Name = "Misc"}
            };

            var subgenres = new List<Genre>()
            {
                new Genre() {Name = "RTS", Parent = genres[0]},
                new Genre() {Name = "TBS", Parent = genres[0]},
                new Genre() {Name = "Rally", Parent = genres[3]},
                new Genre() {Name = "Arcade", Parent = genres[3]},
                new Genre() {Name = "Formula", Parent = genres[3]},
                new Genre() {Name = "Off-road", Parent = genres[3]},
                new Genre() {Name = "FPS", Parent = genres[4]},
                new Genre() {Name = "TPS", Parent = genres[4]},
                new Genre() {Name = "Misc", Parent = genres[4]}
            };

            var platforms = new List<PlatformType>()
            {
                new PlatformType() {Type = "Mobile"},
                new PlatformType() {Type = "Browser"},
                new PlatformType() {Type = "Desktop"},
                new PlatformType() {Type = "Console"},
            };

            context.Genres.AddRange(genres);
            context.Genres.AddRange(subgenres);
            context.PlatformTypes.AddRange(platforms);
            context.SaveChanges();
        }
    }
}
