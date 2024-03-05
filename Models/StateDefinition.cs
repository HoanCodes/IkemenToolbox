using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class StateDefinition : ObservableObject
    {
        [ObservableProperty]
        private int? _id;
        [ObservableProperty]
        private string _name;
        public string DisplayName => !string.IsNullOrEmpty(Name) ? Name : "ID: " + Id.ToString();
        public string MoveType { get; set; }
        public string Physics { get; set; }
        public string Juggle { get; set; }

        public string Ctrl { get; set; }
        public string Anim { get; set; }

        public string VelSet { get; set; }
        public string PowerAdd { get; set; }
        public string SprPriority { get; set; }

        [ObservableProperty]
        private ObservableCollection<State> _states = new();

        public StateDefinition(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}