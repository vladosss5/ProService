using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Context;
using ProServise.Models;
using ReactiveUI;

namespace ProServise.ViewModels;

public class AddUserViewModel : ViewModelBase
{
    private User _newUser = new User();
    private string _login;
    private string _password;
    private string _fName;
    private string _sName;
    private string _lName;
    private int _idPost;
    
    private MyDbContext db = new MyDbContext();
    
    private ObservableCollection<User> _user;
    public ObservableCollection<User> User
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
    
    public string FName
    {
        get => _fName;
        set => this.RaiseAndSetIfChanged(ref _fName, value);
    }
    
    public string SName
    {
        get => _sName;
        set => this.RaiseAndSetIfChanged(ref _sName, value);
    }
    
    public string LName
    {
        get => _lName;
        set => this.RaiseAndSetIfChanged(ref _lName, value);
    }

    public int IdPost
    {
        get => _idPost;
        set => this.RaiseAndSetIfChanged(ref _idPost, value);
    }
    
    private bool _OpenEmployeesPage;

    public ReactiveCommand<Unit, Unit> AddUser { get; }

    public AddUserViewModel()
    {
        User = new ObservableCollection<User>(Helper.GetContext().Users.ToList());
        AddUser = ReactiveCommand.Create(AddUserImpl);
    }

    private void AddUserImpl()
    {
        var user = Helper.GetContext().Users.FirstOrDefault(x=> x.Login == Login);
        
        if (user == null)
        {
            _newUser.Login = _login;
            _newUser.Password = _password;
            _newUser.Fname = _fName;
            _newUser.Sname = _sName;
            _newUser.Lname = _lName;
            Helper.GetContext().Users.Add(_newUser);
            Helper.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandard("Успех", "Пользователь добавлен", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Неверно указаны двнные", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }
}