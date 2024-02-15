using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public class StateDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName => !string.IsNullOrEmpty(Name) ? Name : "ID: " + Id.ToString();

        public string Type { get; set; }
        public string MoveType { get; set; }
        public string Physics { get; set; }
        public int Juggle { get; set; }

        public bool Ctrl { get; set; }
        public int Anim { get; set; }

        public string VelSet { get; set; }
        public int PowerAdd { get; set; }
        public int SprPriority { get; set; }

        public ObservableCollection<State> States { get; set; } = new();

        public StateDefinition(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}