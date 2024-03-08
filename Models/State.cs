using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class State : ObservableObject
    {
        public bool IsEntryState { get; set; }
        [ObservableProperty]
        private string _type;
        [ObservableProperty]
        private string _name;
        public ObservableCollection<StringStringKeyValue> KeyValues { get; set; } = new();
        
        [RelayCommand]
        private void AddKeyValue() => KeyValues.Add(new());
        [RelayCommand]
        private void RemoveKeyValue(StringStringKeyValue keyValue) => KeyValues.Remove(keyValue);
    }
}
