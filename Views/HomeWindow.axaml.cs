using Avalonia.Controls;
using IkemenToolbox.ViewModels;

namespace IkemenToolbox.Views
{
    public partial class HomeWindow : Window
    {
        private readonly HomeViewModel _vm = new();

        public HomeWindow()
        {
            InitializeComponent();
            DataContext = _vm;
        }
    }
}