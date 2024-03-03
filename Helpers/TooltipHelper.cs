using IkemenToolbox.Enums;
using IkemenToolbox.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IkemenToolbox.Helpers
{
    public static class TooltipHelper
    {
        private static readonly List<Tip> _defaultTips = new()
        {
            new Tip("A", "When character is in the air", "Air"),
            new Tip("ctrl", "When player is in control of the character"),
            // Add more
        };

        private static readonly List<Tip> _tips = new();

        public static void Initialize(Fighter fighter)
        {
            _tips.Clear();
            _tips.AddRange(_defaultTips);

            foreach (var commandDefinition in fighter.CommandDefinitions)
            {
                var builder = new StringBuilder();

                foreach (var command in commandDefinition.Commands)
                {
                    builder.AppendLine(command.Command + $" ({command.Time})");
                }

                _tips.Add(new Tip(commandDefinition.Name, builder.ToString().Trim('\r','\n'), type: SystemType.Command));
            }
        }

        public static string GetTip(string key)
        {
            key = key.Trim('"');
            return _tips.FirstOrDefault(x => x.Key.Equals(key))?.Description;
        }
    }
}
