using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Comment> Comments { get; }
        IRepository<Game> Games { get; }
        IRepository<Genre> Genres { get; }
        IRepository<PlatformType> PlatformTypes { get; }
        IRepository<Publisher> Publishers { get; }
        void Save();
    }
}
