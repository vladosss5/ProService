using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.Models;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    public void InfoRepair(Repair rep)
    {
        Window obj = ThisWindow;
        (DataContext as MainWindowViewModel).OpenInfoRepairWindowImpl(rep, obj);
    }
}