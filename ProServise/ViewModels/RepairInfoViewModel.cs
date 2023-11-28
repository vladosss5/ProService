using System.Collections.ObjectModel;
using System.Linq;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class RepairInfoViewModel : ViewModelBase
{
    private Repair _repairInfo;
    private Client _clientInfo;
    private Device _deviceInfo;
    

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

    public RepairInfoViewModel()
    {
        RepairInfo = MainWindowViewModel._SelectRepair;

        ClientInfo = Helper.GetContext().Clients.Where(x => x.IdClient == RepairInfo.IdClient).FirstOrDefault();
        DeviceInfo = Helper.GetContext().Devices.Where(x => x.IdDevice == RepairInfo.IdDevice).FirstOrDefault();
    }
}