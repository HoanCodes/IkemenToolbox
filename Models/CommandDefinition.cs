using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public class CommandDefinition
    {
        public string Name { get; set; }
        public ObservableCollection<InputCommand> Commands { get; set; }
    }
}
