using VpnConnections.Logs;

namespace VpnConnections.Helpers
{
    public static class CommandLine
    {
        private static readonly Logger logger = new Logger(nameof(CommandLine));

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
                logger.LogInfo($"Argument {name} has value {value}");
                return (T)Convert.ChangeType(value, typeof(T));
            }

            logger.LogInfo($"Argument {name} not found, return default");
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

            var argumentsFound = arguments
                .Skip(1)
                .Select(arg => arg.Trim(' ', '\t', '-', '/'))
                .JoinValues(',');

            logger.LogInfo($"Arguments: {string.Join(", ", argumentsFound)}");
            return argumentsFound;
        }

        private static IEnumerable<string> JoinValues(this IEnumerable<string?> values, char jointer)
        {
            var initialized = false;
            string? previous = null;

            foreach (var value in values)
            {
                if (!initialized)
                {
                    previous = value;
                    initialized = true;
                }
                else
                {
                    if ((previous?.EndsWith(jointer) ?? false)
                        || (value?.StartsWith(jointer) ?? false))
                    {
                        previous += value;
                    }
                    else
                    {
                        if (previous != null)
                            yield return previous;

                        previous = value;
                    }
                }
            }

            if (initialized
                && previous != null)
            {
                yield return previous;
            }
        }
    }
}