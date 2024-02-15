using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IkemenToolbox.Helpers;
using IkemenToolbox.Services;
using System;
using System.Threading.Tasks;

namespace IkemenToolbox.ViewModels
{
    public partial class HomeViewModel : ViewModel
    {
        [ObservableProperty]
        private bool _isEditingDefinitionPath = true;

        [ObservableProperty]
        private string _definitionPath = "C:\\Entertainment\\Ikemen_GO-v0.99.0\\chars\\kfm720\\kfm720.def";

        [ObservableProperty]
        private FighterManager _fighterManager = new();

        [RelayCommand]
        private async Task SetDefinitionPathAsync()
        {
            if (string.IsNullOrWhiteSpace(DefinitionPath))
            {
                return;
            }

            DefinitionPath = DefinitionPath.Trim(' ', '"');

            try
            {
                await FighterManager.InitializeAsync(DefinitionPath);
                IsEditingDefinitionPath = false;
            }
            catch (Exception ex)
            {
                await Dialog.ShowAlertAsync(ex.Message, "Error");
            }
        }

        [RelayCommand]
        private void Edit()
        {
            DefinitionPath = FighterManager.Fighter.DefinitionPath;
            IsEditingDefinitionPath = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            IsEditingDefinitionPath = false;
        }
    }
}