using StandUpTimer.Core.ViewModels;

namespace StandUpTimer;

public sealed partial class MainWindow
{
    public MainWindowViewModel ViewModel { get; }

    public MainWindow(MainWindowViewModel viewModel)
    {
        ViewModel = viewModel;
        Title = "Stand Up Timer";
        ExtendsContentIntoTitleBar = false;
        
        InitializeComponent();
    }
}