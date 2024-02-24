using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace IkemenToolbox.Helpers
{
    public static class FileSystem
    {
        public static async Task OpenFolderAsync(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                await Dialog.ShowAlertAsync("Folder does not exist.");
                return;
            }

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            catch
            {
                await Dialog.ShowAlertAsync("Unable to open folder.");
            }

        }
    }
}
