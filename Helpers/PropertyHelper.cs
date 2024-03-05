using IkemenToolbox.Models;
using System;
using System.Linq;
using System.Reflection;

namespace IkemenToolbox.Helpers
{
    public static class PropertyHelper
    {
        static readonly Type IntType = typeof(int);
        static readonly Type BoolType = typeof(bool);

        public static void SetValue<T>(T obj, string propertyName, object value)
        {
            propertyName = propertyName.Replace('.', '_');

            var property = GetProperties(obj).FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (property == null)
            {
                return;
            }

            var propertyType = property.PropertyType;

            if (propertyType == IntType)
            {
                property?.SetValue(obj, int.Parse(value.ToString()));
            }
            else if (propertyType == BoolType)
            {
                property?.SetValue(obj, int.Parse(value.ToString()) == 1);
            }
            else
            {
                var stringValue = value.ToString();
                if (stringValue.StartsWith('"'))
                {
                    property?.SetValue(obj, stringValue.Trim('"'));
                }
                else
                {
                    property?.SetValue(obj, value);
                }
            }
        }

        // This is 100% over-optimizing but I was on a plane and was bored
        static readonly PropertyInfo[] FighterProperties = typeof(Fighter).GetProperties();
        static readonly PropertyInfo[] StateProperties = typeof(OldState).GetProperties();
        static readonly PropertyInfo[] StateDefinitionProperties = typeof(StateDefinition).GetProperties();
        public static PropertyInfo[] GetProperties<T>(T obj)
        {
            var type = typeof(T);

            if (type == typeof(Fighter))
            {
                return FighterProperties;
            }
            else if (type == typeof(OldState))
            {
                return StateProperties;
            }
            else if (type == typeof(StateDefinition))
            {
                return StateDefinitionProperties;
            }
            else
            {
                return typeof(T).GetProperties();
            }
        }
    }
}
