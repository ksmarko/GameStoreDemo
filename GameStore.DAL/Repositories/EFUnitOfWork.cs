using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;

namespace GameStore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private StoreContext db;
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<Game> gameRepository;
        private GenericRepository<Genre> genreRepository;
        private GenericRepository<PlatformType> platformRepository;
        private GenericRepository<Publisher> publisherRepository;

        public EFUnitOfWork(StoreContext context)
        {
            db = context;
        }

        public IRepository<Comment> Comments => commentRepository ?? (commentRepository = new GenericRepository<Comment>(db));

        public IRepository<Game> Games => gameRepository ?? (gameRepository = new GenericRepository<Game>(db));

        public IRepository<Genre> Genres => genreRepository ?? (genreRepository = new GenericRepository<Genre>(db));

        public IRepository<PlatformType> PlatformTypes => platformRepository ?? (platformRepository = new GenericRepository<PlatformType>(db));

        public IRepository<Publisher> Publishers => publisherRepository ?? (publisherRepository = new GenericRepository<Publisher>(db));

        public void Save()
        {
            db.SaveChanges();
        }

        #region Dispose
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
