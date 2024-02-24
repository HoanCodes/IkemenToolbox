using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public class State
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public ObservableCollection<Trigger> Triggers { get; set; } = new ObservableCollection<Trigger>();
        //Vars
    }
}