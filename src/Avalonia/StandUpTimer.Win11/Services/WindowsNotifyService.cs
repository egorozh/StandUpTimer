using Avalonia.Threading;
using Microsoft.Toolkit.Uwp.Notifications;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System;
using System.Threading.Tasks;

namespace StandUpTimer.Win11.Services;

internal class WindowsNotifyService : INotifyService
{
    public async Task Notify(Notify notify)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            var builder = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText(notify.Title)
                .AddText(notify.Message);
                
            builder.Show(toast => { toast.ExpirationTime = DateTime.Now.AddMinutes(5); });
        });
    }
}