using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public class Trigger
    {
        public Trigger(int key, ObservableCollection<string> values)
        {
            Key = key;
            Values = values;
        }

        public int Key { get; set; }
        public ObservableCollection<string> Values { get; set; }
    }
}