using GameStore.DAL.Entities;
using GameStore.DAL.Migrations;
using System.Data.Entity;

namespace GameStore.DAL.EF
{
    public class StoreContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        static StoreContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreContext, Configuration>());
            Database.SetInitializer(new DbInitializer());
        }

        public StoreContext() { } 

        public StoreContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(c => c.Comments)
                .WithRequired(o => o.Game)
                .HasForeignKey(o => o.GameId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Game>()
                .HasMany(c => c.Genres);
        }
    }
}
