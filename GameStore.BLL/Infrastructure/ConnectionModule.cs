using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.BLL.Infrastructure
{
    public class ConnectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<StoreContext>().ToSelf().InRequestScope();
            Bind<IUnitOfWork>().To<EFUnitOfWork>().InRequestScope();
        }
    }
}
