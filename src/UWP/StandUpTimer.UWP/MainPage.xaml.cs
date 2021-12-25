using Autofac;
using StandUpTimer.Core.ViewModels;

namespace StandUpTimer;

public sealed partial class MainPage 
{
    public MainPage()
    {
        this.InitializeComponent();
        DataContext = Startup.GetHost().Resolve<MainWindowViewModel>();
    }
}