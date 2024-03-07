using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace IkemenToolbox.Models
{
    public partial class StateDefinition : ObservableObject
    {
        public string DisplayName => !string.IsNullOrEmpty(Name) ? Name : "ID: " + Id.ToString();

        private int? _id;
        public int? Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _type;
        [ObservableProperty]
        private string _moveType;
        [ObservableProperty]
        private string _physics;

        [ObservableProperty]
        private string _juggle;
        [ObservableProperty]
        private string _velSet;
        [ObservableProperty]
        private string _powerAdd;

        [ObservableProperty]
        private string _ctrl;
        [ObservableProperty]
        private string _anim;
        [ObservableProperty]
        private string _sprPriority;

        [ObservableProperty]
        private ObservableCollection<State> _states = new();

        public StateDefinition() { }
        public StateDefinition(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}