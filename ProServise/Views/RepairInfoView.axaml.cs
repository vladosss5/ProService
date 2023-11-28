using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class RepairInfoView : Window
{
    public RepairInfoView()
    {
        InitializeComponent();
        DataContext = new RepairInfoViewModel();
    }
}