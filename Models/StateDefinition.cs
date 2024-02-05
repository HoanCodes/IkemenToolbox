using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkemenToolbox.Models
{
    public class StateDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<State> States { get; set; } = new();

        public StateDefinition(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}