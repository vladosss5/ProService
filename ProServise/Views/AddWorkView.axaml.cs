using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class AddWorkView : Window
{
    public AddWorkView()
    {
        InitializeComponent();
        DataContext = new AddWorkViewModel();
    }
}