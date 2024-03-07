using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class CommandDefinition : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        public ObservableCollection<CommandInput> CommandInputs { get; set; } = new ObservableCollection<CommandInput>();
    }
}
