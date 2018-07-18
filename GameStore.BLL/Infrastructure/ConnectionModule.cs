using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using Ninject.Modules;

namespace GameStore.BLL.Infrastructure
{
    public class ConnectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<StoreContext>().ToSelf();
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
