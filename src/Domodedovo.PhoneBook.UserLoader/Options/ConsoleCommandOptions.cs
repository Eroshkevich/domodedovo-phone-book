using System.Collections.Generic;

namespace Domodedovo.PhoneBook.UserLoader.Options
{
    public class ConsoleCommandOptions
    {
        public static readonly IDictionary<string, string> SwitchMappings = new Dictionary<string, string>
        {
            {"-cmd", nameof(Command)},
            {"-ucount", nameof(UsersCount)},
            {"-pload", nameof(PictureLoading)}
        };

        public string Command { get; set; }
        public ushort? UsersCount { get; set; }
        public string PictureLoading { get; set; }
    }
}