using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class ProfileView : Window
{
    public ProfileView()
    {
        InitializeComponent();
        DataContext = new ProfileViewModel();
    }
}