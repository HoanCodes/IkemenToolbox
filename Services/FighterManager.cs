using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        }

        [RelayCommand]
        private void AddStateFile() => Fighter.StFiles.Add(string.Empty);
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