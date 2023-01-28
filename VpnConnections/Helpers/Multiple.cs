using VpnConnections.Logging;

namespace VpnConnections.Helpers
{
    public static class Multiple
    {
        public static T Try<T>(
            int maxRetries,
            TimeSpan interval,
            Func<T> func,
            T defaultValueOnError)
        {
            do
            {
                try
                {
                    return func();
                }
                catch
                {
                    maxRetries--;

                    if (maxRetries <= 0)
                    {
                        Logger.LogWarning($"Multiple.Try retries exceeded. Return default value {defaultValueOnError}");
                        return defaultValueOnError;
                    }

                    Logger.LogInfo($"Exception occured. Sleep {interval} before next try.");
                    Thread.Sleep(interval);
                }
            } while (true);
        }

        public static async Task<T> TryAsync<T>(
            int maxRetries,
            TimeSpan interval,
            Func<T> func,
            T defaultValueOnError)
        {
            do
            {
                try
                {
                    return func();
                }
                catch
                {
                    maxRetries--;

                    if (maxRetries <= 0)
                    {
                        Logger.LogWarning($"Multiple.Try retries exceeded. Return default value {defaultValueOnError}");
                        return defaultValueOnError;
                    }

                    Logger.LogInfo($"Exception occured. Sleep {interval} before next try.");
                    await Task.Delay(interval);
                }
            } while (true);
        }
    }
}