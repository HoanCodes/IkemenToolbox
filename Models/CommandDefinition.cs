using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class CommandDefinition : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        public ObservableCollection<CommandInput> CommandInputs { get; set; }
        public CommandDefinition()
        {
            CommandInputs = new ObservableCollection<CommandInput>
            {
                new()
            };
        }
        public CommandDefinition(string name, List<CommandInput> commandInputs)
        {
            Name = name;
            CommandInputs = new ObservableCollection<CommandInput>(commandInputs);
        }
    }
}
