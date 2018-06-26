using AutoMapper;

namespace GameStore.WEB
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                GameStore.BLL.Infrastructure.AutoMapperConfig.Configure(cfg);
                GameStore.WEB.Infrastructure.AutoMapperConfig.Configure(cfg);
            });
        }
    }
}