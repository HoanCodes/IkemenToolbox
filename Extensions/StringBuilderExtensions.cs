using System.Text;

namespace IkemenToolbox.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void AppendSection(this StringBuilder builder, string sectionType, int? id = null, string name = null, string language = null)
        {
            var section = sectionType.Replace('_', ' ');

            if (language != null)
            {
                section = language + "." + section;
            }

            builder.AppendLine($"[{section}]");
        }
        public static void AppendKeyValue(this StringBuilder builder, string key, string value, bool addQuotes = false)
        {
            if (!addQuotes && string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            builder.AppendLine(key + " = " + (addQuotes ? $"\"{value}\"" : value));
        }
    }
}
