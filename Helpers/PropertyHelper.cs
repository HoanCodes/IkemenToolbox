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

        static readonly Type StateType = typeof(State);
        static readonly Type StateDefinitionType = typeof(StateDefinition);

        static readonly PropertyInfo[] StateProperties = typeof(State).GetProperties();
        static readonly PropertyInfo[] StateDefinitionProperties = typeof(StateDefinition).GetProperties();

        public static void SetValue<T>(T obj, string propertyName, object value)
        {
            var property = GetProperties(obj).FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            var propertyType = property?.PropertyType;

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
                property?.SetValue(obj, value);
            }
        }

        private static PropertyInfo[] GetProperties<T>(T obj)
        {
            var type = typeof(T);
            if (type == StateType)
            {
                return StateProperties;
            }
            else if (type == StateDefinitionType)
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
