using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class AddSpareView : Window
{
    public AddSpareView()
    {
        InitializeComponent();
        DataContext = new AddSpareViewModel();
    }
}