using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class State : ObservableObject
    {
        [ObservableProperty]
        private int? _id;
        [ObservableProperty]
        private string _type;
        [ObservableProperty]
        private string _name;
        public ObservableCollection<StringStringKeyValue> KeyValues { get; set; } = new();
    }
}
