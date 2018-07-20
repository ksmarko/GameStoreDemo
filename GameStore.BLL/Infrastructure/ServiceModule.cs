using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameService>().InRequestScope();
            Bind<IGenreService>().To<GenreService>().InRequestScope();
            Bind<IPublisherService>().To<PublisherService>().InRequestScope();
            Bind<ICommentService>().To<CommentService>().InRequestScope();
            Bind<IPlatformService>().To<PlatformService>().InRequestScope();
        }
    }
}
