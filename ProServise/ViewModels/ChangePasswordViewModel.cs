using System.Reactive;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ReactiveUI;

namespace ProServise.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    private string _firstpassword;
    private string _secondpassword;
    private string _oldpassword;
    
    public static User AuthUser { get; set; }
    
    public string OldPassword
    {
        get => _oldpassword;
        set => this.RaiseAndSetIfChanged(ref _oldpassword, value);
    }
    public string FirstPassword
    {
        get => _firstpassword;
        set => this.RaiseAndSetIfChanged(ref _firstpassword, value);
    }
    public string SecondPassword
    {
        get => _secondpassword;
        set => this.RaiseAndSetIfChanged(ref _secondpassword, value);
    }
    
    public ReactiveCommand<Window, Unit> ChangeButton { get; }
    public ReactiveCommand<Window, Unit> Cancel { get; }

    public ChangePasswordViewModel()
    {
        ChangeButton = ReactiveCommand.Create<Window>(SubmitChangeImpl);
        Cancel = ReactiveCommand.Create<Window>(CancelImpl);
    }

    private void CancelImpl(Window obj)
    {
        obj.Close();
    }
    
    private void SubmitChangeImpl(Window obj)
    {
        if (_oldpassword == AuthorizationViewModel.AuthUser.Password)
        {
            if (_oldpassword != _firstpassword)
            {
                if (_firstpassword == _secondpassword)
                {
                    AuthorizationViewModel.AuthUser.Password = _firstpassword;
                    Helper.GetContext().Users.Update(AuthorizationViewModel.AuthUser);
                    Helper.GetContext().SaveChanges();
                    MessageBoxManager.GetMessageBoxStandard("Успех", "Пароль изменён!", ButtonEnum.Ok, Icon.Success).ShowAsync();
                }
                else
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка","Пароли не совпадают",  ButtonEnum.Ok, Icon.Error).ShowAsync();
                    return;
                }
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка","Нельзя использовать старый пароль",  ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }
            
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка","Неверный текущий пароль",  ButtonEnum.Ok, Icon.Error).ShowAsync();
            return;
        }
        
    }
}