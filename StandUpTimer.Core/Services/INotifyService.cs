using System.Threading.Tasks;
using StandUpTimer.Core.Models;

namespace StandUpTimer.Core.Services;

public interface INotifyService
{
    Task Notify(Notify notify);
}