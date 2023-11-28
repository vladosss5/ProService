using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class AddUserView : Window
{
    public AddUserView()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
    }
}