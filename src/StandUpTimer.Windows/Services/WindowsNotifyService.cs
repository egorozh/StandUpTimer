using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Dispatching;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System.Threading.Tasks;

namespace StandUpTimer.Services;

internal class WindowsNotifyService : INotifyService
{
    private readonly DispatcherQueue _thread;

    public WindowsNotifyService()
    {
        _thread = DispatcherQueue.GetForCurrentThread();
    }

    public async Task Notify(Notify notify)
    {
        _thread.TryEnqueue(() =>
        {
            var builder = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText(notify.Title)
                .AddText(notify.Message);

            builder.Show();
        });
    }
}