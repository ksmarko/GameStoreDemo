using Newtonsoft.Json;

namespace GameStore.WEB.Helpers
{
    public static class LogMessageComposer
    {
        public static string Compose<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}