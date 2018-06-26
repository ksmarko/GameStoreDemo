using System.Web.Http;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GameStore.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GameStore.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace GameStore.WEB.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using GameStore.BLL.Infrastructure;
    using GameStore.BLL.Interfaces;
    using GameStore.BLL.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var serviceModule = new ConnectionModule("DefaultConnection");
            var kernel = new StandardKernel(serviceModule);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<IPublisherService>().To<PublisherService>();
            kernel.Bind<ICommentService>().To<CommentService>();
        }        
    }
}