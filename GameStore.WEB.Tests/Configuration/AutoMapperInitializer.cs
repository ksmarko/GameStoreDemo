using AutoMapper;
using GameStore.BLL.Infrastructure;

namespace GameStore.WEB.Tests.Configuration
{
    internal class AutoMapperInitializer
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                //Mapper.Initialize(cfg => AutoMapperConfig.Configure(cfg));
                GameStore.WEB.AutoMapperInitializer.Initialize();
                _isInitialized = true;
            }
        }
    }
}
