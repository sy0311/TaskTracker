using System.Text.RegularExpressions;

namespace TaskTracker.Utilities
{
    public class CommandParser
    {
        public static string GetArgsForAddCommand(string input)
        {

            if (IsValidCommandFormat(input, $@"^{Commands.ADD}\s+""[^""]+""$"))
            {
                LoggerProvider.logger.Information($"Valid {Commands.ADD} command was provided");
                return input.Split('"')[1];
            }

            throw new FormatException($"Invalid command format for '{Commands.ADD}' command.");
        }

        public static (int, string) GetArgsForUpdateCommand(string input)
        {
            if (IsValidCommandFormat(input, $@"^{Commands.UPDATE}\s+\d+\s+""[^""]+""$"))
            {
                int id = int.Parse(input.Split(' ')[1]);
                string taskName = input.Split('"')[1];
                return (id, taskName);
            }

            throw new FormatException($"Invalid command format for '{Commands.UPDATE}' command.");
        }

        public static int GetArgsForCommandWithId(string input, string commandVerb)
        {
            if (IsValidCommandFormat(input, $@"^{commandVerb}\s+\d+$"))
            {
                return int.Parse(input.Split(' ')[1]);
            }
            throw new FormatException($"Invalid command format for '{commandVerb}' command.");
        }

        private static bool IsValidCommandFormat(string input, string regexPattern)  //todo change to private
        {
            // Check if the command line is of the correct format
            return Regex.IsMatch(input.Trim(), regexPattern);
        }
    }
}
