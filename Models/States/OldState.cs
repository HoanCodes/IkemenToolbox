using System;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    [Obsolete("Current State use a collection of key values instead for flexibility")]
    public class OldState
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public ObservableCollection<Trigger> Triggers { get; set; } = new ObservableCollection<Trigger>();
        //Vars
    }
}