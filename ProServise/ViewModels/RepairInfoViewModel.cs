using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class RepairInfoViewModel : ViewModelBase
{
    private Repair _repairInfo;
    private Client _clientInfo;
    private Device _deviceInfo;
    
    private ObservableCollection<Work> _works;
    private ObservableCollection<Spare> _spares;

    public Repair RepairInfo
    {
        get => _repairInfo;
        set => this.RaiseAndSetIfChanged(ref _repairInfo, value);
    }

    public Client ClientInfo
    {
        get => _clientInfo;
        set => this.RaiseAndSetIfChanged(ref _clientInfo, value);
    }

    public Device DeviceInfo
    {
        get => _deviceInfo;
        set => this.RaiseAndSetIfChanged(ref _deviceInfo, value);
    }

    
    public ObservableCollection<Work> Works
    {
        get => _works;
        set => this.RaiseAndSetIfChanged(ref _works, value);
    }

    public ObservableCollection<Spare> Spares
    {
        get => _spares;
        set => this.RaiseAndSetIfChanged(ref _spares, value);
    }


    public ReactiveCommand<Window, Unit> AddWork { get; }
    public ReactiveCommand<Window, Unit> AddSpare { get; }
    public ReactiveCommand<Window, Unit> Cancel { get; }
    public ReactiveCommand<Window, Unit> Save { get; }
    

    public RepairInfoViewModel()
    {
        RepairInfo = MainWindowViewModel._SelectRepair;

        ClientInfo = Helper.GetContext().Clients.Where(x => x.IdClient == RepairInfo.IdClient).FirstOrDefault();
        DeviceInfo = Helper.GetContext().Devices.Where(x => x.IdDevice == RepairInfo.IdDevice).FirstOrDefault();

        Works = new ObservableCollection<Work>(Helper.GetContext().Works.ToList());
        Spares = new ObservableCollection<Spare>(Helper.GetContext().Spares.ToList());

        AddWork = ReactiveCommand.Create<Window>(AddWorkImpl);
        AddSpare = ReactiveCommand.Create<Window>(AddSpareImpl);
        Cancel = ReactiveCommand.Create<Window>(CancelImpl);
        Save = ReactiveCommand.Create<Window>(SaveImpl);
    }

    private void SaveImpl(Window obj)
    {
        throw new System.NotImplementedException();
    }

    private void CancelImpl(Window obj)
    {
        MainWindowView mwv = new MainWindowView();
        mwv.Show();
        obj.Close();
    }

    private void AddWorkImpl(Window obj)
    {
        AddWorkView awv = new AddWorkView();
        awv.Show();
        obj.Close();
    }
    
    private void AddSpareImpl(Window obj)
    {
        AddSpareView asv = new AddSpareView();
        asv.Show();
        obj.Close();
    }
}