using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Dispatching;
using StandUpTimer.Core.Models;
using StandUpTimer.Core.Services;
using System;
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
                .AddText(notify.Message)
                .AddInlineImage(GetImageUrl(notify)); 

            var audio = GetAudioUrl(notify);

            if (audio != null)
                builder.AddAudio(audio);


            builder.Show(toast => { toast.ExpirationTime = DateTime.Now.AddMinutes(5); });
        });
    }

    private static Uri? GetAudioUrl(Notify notify) => notify switch
    {
        EndWorkDayNotify => null,
        GoSitNotify => new Uri("ms-appx:///Assets/toSit.mp3"),
        GoStandUpNotify => new Uri("ms-appx:///Assets/toStand.mp3"),
        StartWorkDayNotify => null,
        _ => null
    };

    private static Uri? GetImageUrl(Notify notify) => notify switch
    {
        EndWorkDayNotify => null,
        GoSitNotify => new Uri("ms-appx:///Assets/sit.png"),
        GoStandUpNotify => new Uri("ms-appx:///Assets/stand.png"),
        StartWorkDayNotify => null,
        _ => null
    };
}