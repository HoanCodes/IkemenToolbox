using System.Text;

namespace IkemenToolbox.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void AppendSection(this StringBuilder builder, string sectionType, int? id = null, string name = null)
        {
            builder.AppendLine($"[{sectionType}]");
        }
        public static void AppendKeyValue(this StringBuilder builder, string key, string value, bool isString = false)
        {
            if (!isString && string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            builder.AppendLine(key + " = " + (isString ? $"\"{value}\"" : value));
        }
    }
}
