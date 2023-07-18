using System.Diagnostics;
using System.Globalization;
using VpnConnections.Helpers;

namespace VpnConnections.Logs
{
    public static partial class Logging
    {
        private static readonly string ApplicationLogPath = GetLogPath();
        private static readonly IReadOnlySet<string> Modules = new HashSet<string>(new[] { "*" });

        private static bool enabled;

        static Logging()
        {
            if (!Directory.Exists(ApplicationLogPath))
            {
                Directory.CreateDirectory(ApplicationLogPath);
            }

            Modules = GetModulesToLog();
            Enabled = CommandLine.HasArgument("log");
            Trace.AutoFlush = true;
        }

        public static bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled != value)
                {
                    enabled = value;

                    foreach (var disposable in Trace.Listeners.OfType<IDisposable>())
                    {
                        disposable.Dispose();
                    }

                    Trace.Listeners.Clear();

                    if (value)
                    {
                        // Using InvariantCulture since this is used for a log file name
                        var logFilePath = Path.Combine(ApplicationLogPath, "Log_" + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".txt");

                        Trace.Listeners.Add(new DefaultTraceListener());
                        Trace.Listeners.Add(new TextWriterTraceListener(logFilePath));
                    }
                }
            }
        }

        public static void LogError(string message, string? className = null)
        {
            Log(message, "ERROR", className);
        }

        public static void LogError(string message, Exception ex, string? className = null)
        {
            Log(message + Environment.NewLine +
                ex?.Message + Environment.NewLine +
                "Inner exception: " + Environment.NewLine +
                ex?.InnerException?.Message + Environment.NewLine +
                "Stack trace: " + Environment.NewLine +
                ex?.StackTrace,
                "ERROR", className);
        }

        public static void LogInfo(string message, string? className = null)
        {
            Log(message, "INFO", className);
        }

        public static void LogWarning(string message, string? className = null)
        {
            Log(message, "WARNING", className);
        }

        private static CallerInfo GetCallerInfo(string? className)
        {
            var stackTrace = new StackTrace();
            var frame = GetFrame(stackTrace, 3);
            var callerInfo = CallerInfo.From(frame, className);

            if (callerInfo.MethodName.StartsWith("Log"))
            {
                frame = GetFrame(stackTrace, 4);
                callerInfo = CallerInfo.From(frame, className);
            }

            return callerInfo;

            static StackFrame? GetFrame(StackTrace stackTrace, int depth)
            {
                StackFrame? frame;

                do
                {
                    frame = stackTrace.GetFrame(depth--);
                } while (frame == null && depth > 0);

                return frame;
            }
        }

        private static string GetLogPath()
        {
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            return Path.Combine(appFolder, Application.ProductName);
        }

        private static HashSet<string> GetModulesToLog()
        {
            var moduleNames = CommandLine.GetArgumentValue("log")
                ?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                ?? new[] { "*" };

            return new HashSet<string>(moduleNames, StringComparer.OrdinalIgnoreCase);
        }

        private static void Log(string message, string type, string? className)
        {
            if (!enabled)
                return;

            var callerInfo = GetCallerInfo(className);

            if (Modules.Contains("*")
                || Modules.Contains(callerInfo.ClassName))
            {
                Trace.WriteLine(type + ": " + DateTime.Now.TimeOfDay);
                Trace.Indent();
                Trace.WriteLine(callerInfo);
                Trace.WriteLine(message);
                Trace.Unindent();
            }
        }
    }
}