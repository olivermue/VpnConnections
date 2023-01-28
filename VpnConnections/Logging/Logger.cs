using System.Diagnostics;
using System.Globalization;
using VpnConnections.Helpers;

namespace VpnConnections.Logging
{
    public static class Logger
    {
        private static readonly string ApplicationLogPath = GetLogPath();

        static Logger()
        {
            if (!Directory.Exists(ApplicationLogPath))
            {
                Directory.CreateDirectory(ApplicationLogPath);
            }

            if (CommandLine.HasArgument("logfile"))
            {
                // Using InvariantCulture since this is used for a log file name
                var logFilePath = Path.Combine(ApplicationLogPath, "Log_" + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".txt");

                Trace.Listeners.Add(new TextWriterTraceListener(logFilePath));
            }

            Trace.AutoFlush = true;
        }

        public static void LogError(string message)
        {
            Log(message, "ERROR");
        }

        public static void LogError(string message, Exception ex)
        {
            Log(message + Environment.NewLine +
                ex?.Message + Environment.NewLine +
                "Inner exception: " + Environment.NewLine +
                ex?.InnerException?.Message + Environment.NewLine +
                "Stack trace: " + Environment.NewLine +
                ex?.StackTrace,
                "ERROR");
        }

        public static void LogInfo(string message)
        {
            Log(message, "INFO");
        }

        public static void LogWarning(string message)
        {
            Log(message, "WARNING");
        }

        private static string GetCallerInfo()
        {
            var stackTrace = new StackTrace();

            var methodName = stackTrace.GetFrame(3)?.GetMethod();
            var className = methodName?.DeclaringType?.Name;
            return "[Method]: " + methodName?.Name + " [Class]: " + className;
        }

        private static string GetLogPath()
        {
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return Path.Combine(appFolder, Application.ProductName);
        }

        private static void Log(string message, string type)
        {
            Trace.WriteLine(type + ": " + DateTime.Now.TimeOfDay);
            Trace.Indent();
            Trace.WriteLine(GetCallerInfo());
            Trace.WriteLine(message);
            Trace.Unindent();
        }
    }
}