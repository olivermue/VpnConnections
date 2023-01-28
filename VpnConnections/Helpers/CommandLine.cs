using VpnConnections.Logging;

namespace VpnConnections.Helpers
{
    public static class CommandLine
    {
        public static string? GetArgumentValue(string name)
            => GetArgumentValue<string>(name);

        public static T? GetArgumentValue<T>(string name)
        {
            var value = GetCommandLineArgs()
                .SkipWhile(arg => !StringComparer.OrdinalIgnoreCase.Equals(arg, name))
                .Skip(1)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(value))
            {
                Logger.LogInfo($"Argument {name} has value {value}");
                return (T)Convert.ChangeType(value, typeof(T));
            }

            Logger.LogInfo($"Argument {name} not found, return default");
            return default;
        }

        public static bool HasArgument(string name)
        {
            return GetCommandLineArgs()
                .Any(arg => StringComparer.OrdinalIgnoreCase.Equals(arg, name));
        }

        private static IEnumerable<string> GetCommandLineArgs()
        {
            var arguments = Environment.GetCommandLineArgs();

            var argumentsFound =  arguments
                .Skip(1)
                .Select(arg => arg.Trim(' ', '\t', '-', '/'));

            Logger.LogInfo($"Arguments: {string.Join(", ", argumentsFound)}");
            return argumentsFound;
        }
    }
}