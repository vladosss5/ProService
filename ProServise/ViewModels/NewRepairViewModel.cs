using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class NewRepairViewModel : ViewModelBase
{
    private ObservableCollection<Device> _devices;
    private ObservableCollection<Client> _clients;
    private ObservableCollection<Repair> _repairs;

    private Repair _newRepair = new Repair();
    private Client _newClient = new Client();
    private Device _newDevice = new Device();

    private string _type;
    private string _manufacturer;
    private string _model;
    private string _serialNumber;
    private string _descriptionBreaking;

    private string _fName;
    private string _sName;
    private string _lName;
    private string _phoneNumber;
    private string _eMail;
    private string _address;
    
    public static User AuthUserNow { get; set; }

    public ObservableCollection<Device> Devices
    {
        get => _devices;
        set => this.RaiseAndSetIfChanged(ref _devices, value);
    }
    
    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set => this.RaiseAndSetIfChanged(ref _clients, value);
    }
    
    public ObservableCollection<Repair> Repairs
    {
        get => _repairs;
        set => this.RaiseAndSetIfChanged(ref _repairs, value);
    }

    public string Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public string Manufacturer
    {
        get => _manufacturer;
        set => this.RaiseAndSetIfChanged(ref _manufacturer, value);
    }

    public string Model
    {
        get => _model;
        set => this.RaiseAndSetIfChanged(ref _model, value);
    }

    public string SerialNumber
    {
        get => _serialNumber;
        set => this.RaiseAndSetIfChanged(ref _serialNumber, value);
    }

    public string DescriptionBreaking
    {
        get => _descriptionBreaking;
        set => this.RaiseAndSetIfChanged(ref _descriptionBreaking, value);
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

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
    }

    public string EMail
    {
        get => _eMail;
        set => this.RaiseAndSetIfChanged(ref _eMail, value);
    }

    public string Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }
    
    public ReactiveCommand<Window, Unit> AddRepair { get; }
    public ReactiveCommand<Window, Unit> Cancel { get; }
    
    public NewRepairViewModel()
    {
        AuthUserNow = AuthorizationViewModel.AuthUser;
        
        Devices = new ObservableCollection<Device>(Helper.GetContext().Devices.ToList());
        Clients = new ObservableCollection<Client>(Helper.GetContext().Clients.ToList());
        Repairs = new ObservableCollection<Repair>(Helper.GetContext().Repairs.ToList());

        AddRepair = ReactiveCommand.Create<Window>(AddRepairImpl);
        Cancel = ReactiveCommand.Create<Window>(CancelImpl);
    }

    private void CancelImpl(Window obj)
    {
        MainWindowView mnv = new MainWindowView();
        mnv.Show();
        obj.Close();
    }

    public void AddRepairImpl(Window obj)
    {
        var context = Helper.GetContext();

        var client = context.Clients.FirstOrDefault(x => x.Fname == FName && x.PhoneNumber == PhoneNumber);

        try
        {
            if (client == null)
            {
                _newClient.Fname = FName;
                _newClient.Sname = SName;
                _newClient.Lname = LName;
                _newClient.PhoneNumber = PhoneNumber;
                _newClient.Address = Address;
                _newClient.Email = EMail;

                context.Clients.Add(_newClient);
                context.SaveChanges();
            }

            _newDevice.Manufacturer = Manufacturer;
            _newDevice.Model = Model;
            _newDevice.SerialNumber = SerialNumber;
            _newDevice.Type = Type;

            context.Devices.Add(_newDevice);
            context.SaveChanges();
        
            _newRepair.DateReception = DateTime.Now;
            _newRepair.IdClient = context.Clients.OrderByDescending(x => x.IdClient).FirstOrDefault().IdClient;
            _newRepair.IdDevice = context.Devices.OrderByDescending(x => x.IdDevice).FirstOrDefault().IdDevice;
            if (AuthUserNow != null)
            {
                _newRepair.IdUser = AuthUserNow.IdUser;
            }
            else
            {
                _newRepair.IdUser = 1;
            }

            _newRepair.Status = "Принят";
            _newRepair.DescriptionBreaking = DescriptionBreaking;

            context.Repairs.Add(_newRepair);
            context.SaveChanges();
            
            MainWindowView mwv = new MainWindowView();
            mwv.Show();
            obj.Close();
            
            MessageBoxManager.GetMessageBoxStandard("Успех", "Ремонт добавлен!", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        catch (Exception e)
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", $"{e}", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }
}