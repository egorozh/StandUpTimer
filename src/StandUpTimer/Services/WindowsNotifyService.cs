using System;
using System.Threading.Tasks;
#if WIN10
using Avalonia.Threading;
using Microsoft.Toolkit.Uwp.Notifications;
#endif
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;

namespace StandUpTimer.Services;

internal class WindowsNotifyService : INotifyService
{
    public async Task Notify(Notify notify)
    {
#if WIN10
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            var builder = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText(notify.Title)
                .AddText(notify.Message);
                
            builder.Show(toast => { toast.ExpirationTime = DateTime.Now.AddMinutes(5); });
        });
#endif
    }
}