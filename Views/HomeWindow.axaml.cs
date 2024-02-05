using Avalonia.Controls;
using IkemenToolbox.ViewModels;
using System;
using System.Collections.Generic;

namespace IkemenToolbox.Views
{
    public partial class HomeWindow : Window
    {
        private readonly HomeViewModel _vm = new();

        public HomeWindow()
        {
            InitializeComponent();
            DataContext = _vm;
            MainTabControl.SelectedIndex = 2;
        }
    }
}