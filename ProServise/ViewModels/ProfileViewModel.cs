using System.Reactive;
using Avalonia.Controls;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class ProfileViewModel : ViewModelBase
{
    private static User AuthUserNow { get; set; }
    private User _user;
    
    public ReactiveCommand<Window, Unit> GoBack { get; }
    public ReactiveCommand<Window, Unit> OpenWindowChangePassword { get; }
    public ProfileViewModel()
    {
        AuthUserNow = AuthorizationViewModel.AuthUser;
        GoBack = ReactiveCommand.Create<Window>(GoBackImpl);
        OpenWindowChangePassword = ReactiveCommand.Create<Window>(OpenWindowChangePasswordImpl);
    }

    private void OpenWindowChangePasswordImpl(Window obj)
    {
        ChangePasswordView changePass = new ChangePasswordView();
        changePass.Show();
    }

    private void GoBackImpl(Window obj)
    {
        MainWindowView mwv = new MainWindowView();
        mwv.Show();
        obj.Close();
    }
}