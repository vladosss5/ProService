using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class AuthorizationViewModel : ViewModelBase
{
    private string _password;
    private string _login;
    public static User AuthUser { get; set; }

    private User _user;
    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
    public string Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ReactiveCommand<Window, Unit> ButtonEnter { get; }

    public AuthorizationViewModel()
    {
        ButtonEnter = ReactiveCommand.Create<Window>(OpenWindowImpl);
    }
    

    private void OpenWindowImpl(Window obj)
    {
        User user = null;
        user = Helper.GetContext().Users.SingleOrDefault(x => x.Login == Login & x.Password == Password);
        
        if (user != null)
        {
            MainWindowView mnv = new MainWindowView();
            mnv.Show();
            obj.Close();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Вы ввели неверный логин или пароль", ButtonEnum.Ok).ShowAsync();
        }
    }
}