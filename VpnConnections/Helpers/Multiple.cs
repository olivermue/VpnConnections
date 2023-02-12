using VpnConnections.Logs;

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
                        Logging.LogWarning($"Multiple.Try retries exceeded. Return default value {defaultValueOnError}", "Multiple.Try");
                        return defaultValueOnError;
                    }

                    Logging.LogInfo($"Exception occured. Sleep {interval} before next try.", "Multiple.Try");
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
                        Logging.LogWarning($"Multiple.Try retries exceeded. Return default value {defaultValueOnError}", "Multiple.Try");
                        return defaultValueOnError;
                    }

                    Logging.LogInfo($"Exception occured. Sleep {interval} before next try.", "Multiple.Try");
                    await Task.Delay(interval);
                }
            } while (true);
        }
    }
}