using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using Ninject.Modules;

namespace GameStore.BLL.Infrastructure
{
    public class ConnectionModule : NinjectModule
    {
        private readonly string connectionString;

        public ConnectionModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<StoreContext>().ToSelf().WithConstructorArgument(connectionString);
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
