using Ninject.Modules;
using NLog;

namespace GameStore.WEB.Infrastructure
{
    public class LoggerConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(p => NLog.LogManager.GetCurrentClassLogger());
        }
    }
}