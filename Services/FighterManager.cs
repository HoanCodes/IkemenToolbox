using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IkemenToolbox.Extensions;
using IkemenToolbox.Helpers;
using IkemenToolbox.Models;
using System;
using System.IO;
using System.Threading.Tasks;


namespace IkemenToolbox.Services
{
    public partial class FighterManager : ObservableObject
    {
        public static FighterManager Instance;

        [ObservableProperty]
        private Fighter _fighter = new Fighter();

        public FighterManager() => Instance = this;

        public async Task InitializeAsync(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            Fighter = new Fighter();
            await Fighter.InitializeAsync(path);

            TooltipHelper.Initialize(Fighter);
        }

        //Definition
        [RelayCommand]
        private void AddStateFile() => Fighter.StFiles.Add(string.Empty);

        //Command
        [RelayCommand]
        private void AddEntryState() => Fighter.EntryStates.AddToStart(new());
        [RelayCommand]
        private void AddCommandDefinition() => Fighter.CommandDefinitions.AddToStart(new());
        [RelayCommand]
        private void AddCommandInput(CommandDefinition command) => command?.CommandInputs.Add(new());

        //State
        [RelayCommand]
        private void AddStateDefinition() => Fighter.StateDefinitions.AddToStart(new());
        [RelayCommand]
        private void AddState(StateDefinition stateDefinition) => stateDefinition.States.AddToStart(new());
        [RelayCommand]
        private void AddStateKeyValue(State state) => state.KeyValues.Add(new());

        //Quotes
        [RelayCommand]
        private void AddQuote() => Fighter.Quotes.Add(string.Empty);
        [RelayCommand]
        private void AddJapaneseQuote() => Fighter.Ja_Quotes.Add(string.Empty);

        [RelayCommand]
        private async Task ExportFileAsync(string fileName)
        {
            try
            {
                var writer = new FighterWriter(Fighter);
                switch (fileName)
                {
                    case CommonFile.def:
                        await writer.WriteDefAsync();
                        break;
                    case CommonFile.cns:
                        await writer.WriteCnsAsync();
                        break;
                    default:
                        throw new InvalidOperationException("File Type not supported");
                }

                await Dialog.ShowAlertAsync(fileName + " EXPORTED");
            }
            catch (Exception ex)
            {
                await Dialog.ShowAlertAsync(ex.Message, "Unable to Export");
            }
        }
    }
}