using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Infrastructure;

namespace GameStore.BLL.Tests.Configuration
{
    internal class AutoMapperInitializer
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                Mapper.Initialize(cfg => AutoMapperConfig.Configure(cfg));
                _isInitialized = true;
            }
        }
    }
}
