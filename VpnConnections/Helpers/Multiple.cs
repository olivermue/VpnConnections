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
                        return defaultValueOnError;

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
                        return defaultValueOnError;

                    await Task.Delay(interval);
                }
            } while (true);
        }
    }
}