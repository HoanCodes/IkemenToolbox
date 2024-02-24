using System;
using System.Globalization;

namespace IkemenToolbox.Converters
{
    public class StringIsNullOrWhiteSpaceConverter : OneWayConverter
    {
        public bool IsOpposite { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is string ? IsOpposite ? !string.IsNullOrWhiteSpace(value as string) : string.IsNullOrWhiteSpace(value as string) : false;
    }
}
