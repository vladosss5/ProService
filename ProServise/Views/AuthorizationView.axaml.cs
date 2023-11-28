using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.Context;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class AuthorizationView : Window
{
    public AuthorizationView()
    {
        InitializeComponent();
        DataContext = new AuthorizationViewModel();
        MyDbContext MyDbContext = new MyDbContext();
        MyDbContext.Database.EnsureCreated();
    }
}