﻿using AutoMapper;
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
