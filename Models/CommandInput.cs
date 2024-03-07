using CommunityToolkit.Mvvm.ComponentModel;

namespace IkemenToolbox.Models
{
    public partial class CommandInput : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _command;
        [ObservableProperty]
        private int? _time;
    }
}