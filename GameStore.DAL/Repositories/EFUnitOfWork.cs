using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

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

        public EFUnitOfWork(string connectionString)
        {
            db = new StoreContext(connectionString);
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new GenericRepository<Comment>(db);
                return commentRepository;
            }
        }

        public IRepository<Game> Games
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GenericRepository<Game>(db);
                return gameRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenericRepository<Genre>(db);
                return genreRepository;
            }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (platformRepository == null)
                    platformRepository = new GenericRepository<PlatformType>(db);
                return platformRepository;
            }
        }

        public IRepository<Publisher> Publishers
        {
            get
            {
                if (publisherRepository == null)
                    publisherRepository = new GenericRepository<Publisher>(db);
                return publisherRepository;
            }
        }

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
