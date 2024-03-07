using IkemenToolbox.Helpers;
using IkemenToolbox.Models;
using System;
using System.Linq;
using System.Reflection;

namespace IkemenToolbox.Extensions
{
    public static class StringExtensions
    {
        public static string SplitAndGetLast(this string text, char separator) => text[(text.LastIndexOf(separator) + 1)..];

        public static PropertyInfo GetPropertyInfo(this string propertyName, Type type) => propertyName?.GetPropertyInfo(type.GetProperties());

        public static PropertyInfo GetPropertyInfo(this string propertyName, PropertyInfo[] properties) => properties?.FirstOrDefault(x => x.Name.Equals(propertyName?.SplitAndGetLast('.'), StringComparison.OrdinalIgnoreCase));

        public static bool IsHeader(this string value) => value.StartsWith("[");

        public static bool TryGetHeader(this string value, out Header section)
        {
            section = null;

            if (!value.IsHeader())
            {
                return false;
            }

            value = value[1..^1];   //Remove brackets

            var name = string.Empty;
            var nameSplit = value.Split(',', 2);
            if (nameSplit.Length == 2)
            {
                name = nameSplit[1].Trim();
            }

            int? id = null;
            var type = string.Empty;
            var idSplit = nameSplit[0].Split(' ');
            if (idSplit.Length > 1)
            {
                if (int.TryParse(idSplit[^1], out int result))
                {
                    id = result;

                    for (var i = 0; i < idSplit.Length - 1; i++)
                    {
                        type += idSplit[i] + ".";
                    }
                }
                else
                {
                    for (var i = 0; i < idSplit.Length; i++)
                    {
                        type += idSplit[i] + ".";
                    }
                }
                type = type.Trim('.');
            }
            else
            {
                type = idSplit[0];
            }

            type = type.Replace('.', '_');

            if (!char.IsUpper(type[0]))
            {
                type = char.ToUpper(type[0]) + type[1..];
            }

            section = new Header
            {
                Id = id,
                Name = name,
                Type = type,
            };

            return true;
        }

        public static bool TryGetKeyValue(this string line, out string key, out string value)
        {
            key = null;
            value = null;

            var split = line.Split('=', 2);
            if (split.Length != 2)
            {
                return false;
            }

            key = split[0].Trim();
            value = split[1].Trim();

            return true;
        }

        public static bool TryGetTip(this string text, out string tip)
        {
            tip = TooltipHelper.GetTip(text);

            if (tip == null)
            {
                return false;
            }

            return true;
        }
    }
}