namespace VpnConnections.Logs
{
    public record Logger(string ClassName)
    {
        public void LogError(string message)
        {
            Logging.LogError(message, ClassName);
        }

        public void LogWarning(string message)
        {
            Logging.LogWarning(message, ClassName);
        }
        public void LogInfo(string message)
        {
            Logging.LogInfo(message, ClassName);
        }

        public void LogError(string message, Exception exception)
        {
            Logging.LogError(message, exception, ClassName);
        }
    }
}