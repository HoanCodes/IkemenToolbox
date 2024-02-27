using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;

namespace IkemenToolbox.Models
{
    public partial class StringStringKeyValue : ObservableObject
    {
        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private string _value;

        public StringStringKeyValue() { }

        public StringStringKeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public partial class StringIntKeyValue : ObservableObject
    {
        [ObservableProperty]
        private string _key;

        [ObservableProperty]
        private int _value;

        public StringIntKeyValue(string key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
