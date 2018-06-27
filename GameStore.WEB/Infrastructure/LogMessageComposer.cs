using Newtonsoft.Json;

namespace GameStore.WEB.Infrastructure
{
    public static class LogMessageComposer
    {
        public static string Compose<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}