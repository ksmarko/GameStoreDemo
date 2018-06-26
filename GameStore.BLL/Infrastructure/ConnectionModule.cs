using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
