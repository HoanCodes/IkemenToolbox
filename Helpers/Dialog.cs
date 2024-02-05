using MsBox.Avalonia;
using System.Threading.Tasks;

namespace IkemenToolbox.Helpers
{
    public static class Dialog
    {
        public static async Task ShowAlertAsync(string message, string title = "")
        {
            await MessageBoxManager.GetMessageBoxStandard(title, message).ShowAsync();
        }
    }
}