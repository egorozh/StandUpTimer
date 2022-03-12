using Avalonia.Controls;

namespace StandUpTimer.UI.Services;

public interface IWindowService
{
    void ShowWindow();
    Window CreateWindow();
}