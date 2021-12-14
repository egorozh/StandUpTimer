using System.Threading.Tasks;
using Avalonia.Threading;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;

namespace StandUpTimer.Services;

internal class DefaultNotifyService : INotifyService
{
    public async Task Notify(Notify notify)
    {
#if !WIN10
        await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(notify.Title, notify.Message);

            var result = await messageBoxStandardWindow.Show();
        });
#endif

    }
}