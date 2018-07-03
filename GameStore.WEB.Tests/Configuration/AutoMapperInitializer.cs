namespace GameStore.WEB.Tests.Configuration
{
    internal class AutoMapperInitializer
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                GameStore.WEB.AutoMapperInitializer.Initialize();
                _isInitialized = true;
            }
        }
    }
}
