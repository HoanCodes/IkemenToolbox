using CommunityToolkit.Mvvm.ComponentModel;
using IkemenToolbox.Models;
using System.IO;
using System.Threading.Tasks;

namespace IkemenToolbox.Services
{
    public partial class FighterManager : ObservableObject
    {
        public static FighterManager Instance;

        [ObservableProperty]
        private Fighter _fighter;

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
    }
}