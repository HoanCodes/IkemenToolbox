using System.Collections.Generic;

namespace IkemenToolbox.Extensions
{
    public static class DictionaryExtensions
    {
        public static T2 TryGetValue<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return default;
        }
    }
}