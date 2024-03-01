using System;
using System.Globalization;

namespace IkemenToolbox.Converters
{
    public class BoolOppositeConverter : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return !boolean;
            }
            return value;
        }
    }
}
