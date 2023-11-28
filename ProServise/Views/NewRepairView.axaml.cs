using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProServise.ViewModels;

namespace ProServise.Views;

public partial class NewRepairView : Window
{
    public NewRepairView()
    {
        InitializeComponent();
        DataContext = new NewRepairViewModel();
    }
}