using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProServise.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<Repair> _repairs;
    private ObservableCollection<Device> _devices;
    private ObservableCollection<Client> _clients;
    private ObservableCollection<User> _users;
    
    
    public static Repair _SelectRepair;
    
    private string _searchTBox_OnTextChanged;

    public string SearchTBox_OnTextChanged
    {
        get => _searchTBox_OnTextChanged;
        set
        {
            Repairs = new ObservableCollection<Repair>(Helper.GetContext().Repairs.
                Where(i => i.IdClientNavigation.Sname.Contains(value)));
            this.RaiseAndSetIfChanged(ref _searchTBox_OnTextChanged, value);
        }
    }

    public ObservableCollection<Repair> Repairs
    {
        get => _repairs;
        set => this.RaiseAndSetIfChanged(ref _repairs, value);
    }
    
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
    
    public ObservableCollection<User> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }
    
    public ReactiveCommand<Window, Unit> AddRepair { get; }
    public ReactiveCommand<Window, Unit> AddUser { get; }
    public ReactiveCommand<Window, Unit> CreateReport { get; }
    public ReactiveCommand<Window, Unit> ButtonProfile { get; }
    
    public MainWindowViewModel()
    {
        AddRepair = ReactiveCommand.Create<Window>(OpenRepairWindowImpl);
        AddUser = ReactiveCommand.Create<Window>(AddUserImpl);
        CreateReport = ReactiveCommand.Create<Window>(CreateReportImpl);
        ButtonProfile = ReactiveCommand.Create<Window>(OpenProfileWindow);
        Repairs = new ObservableCollection<Repair>(Helper.GetContext().Repairs.
            Include(e => e.IdClientNavigation).ToList());
        Devices = new ObservableCollection<Device>(Helper.GetContext().Devices.ToList());
        Clients = new ObservableCollection<Client>(Helper.GetContext().Clients.ToList());
        Users = new ObservableCollection<User>(Helper.GetContext().Users.ToList());
    }

    private void CreateReportImpl(Window obj)
    {
        using(ExcelHelper helper = new ExcelHelper())
        {
            if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, @"C:\Users\v\Documents\Отчёт о заказах.xlsx")))
            {
                int i = 0;
                var allOrders = Repairs.ToList().OrderBy(p => p.DateReception).ToList();
                var application = new Excel.Application();
                string[] month = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", 
                                                            "Август", "Сентябрь", "Окрябрь", "Ноябрь", "Декабрь" };
                
                application.SheetsInNewWorkbook = month.Length;

                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                
                for (int j = 0; j < month.Length; ++j)
                {
                    int counter = 0;
                    int startRowIndex = 1;
                    
                    Excel.Worksheet worksheet = application.Worksheets.Item[j + 1];
                    worksheet.Name = month[j];

                    worksheet.Cells[1][startRowIndex] = "Номер";
                    worksheet.Cells[2][startRowIndex] = "Дата приёма";
                    worksheet.Cells[3][startRowIndex] = "Производитель";
                    worksheet.Cells[4][startRowIndex] = "Модель";
                    worksheet.Cells[5][startRowIndex] = "Тип";
                    worksheet.Cells[6][startRowIndex] = "Серийный номер";
                    worksheet.Cells[7][startRowIndex] = "Имя";
                    worksheet.Cells[8][startRowIndex] = "Фамилия";
                    worksheet.Cells[9][startRowIndex] = "Отчество";
                    worksheet.Cells[10][startRowIndex] = "Номер телефона";
                    worksheet.Cells[11][startRowIndex] = "Адрес";
                    worksheet.Cells[12][startRowIndex] = "Эл.Почта";

                    startRowIndex++;

                    while (allOrders.Count > i)
                    {
                        if (allOrders[i].DateReception.Month == j + 1)
                        {
                            worksheet.Cells[1][startRowIndex] = allOrders[i].IdRepair;
                            worksheet.Cells[2][startRowIndex] = allOrders[i].DateReception.ToString("yy-MM-dd");
                            worksheet.Cells[3][startRowIndex] = allOrders[i].IdDeviceNavigation.Manufacturer;
                            worksheet.Cells[4][startRowIndex] = allOrders[i].IdDeviceNavigation.Model;
                            worksheet.Cells[5][startRowIndex] = allOrders[i].IdDeviceNavigation.Type;
                            worksheet.Cells[6][startRowIndex] = allOrders[i].IdDeviceNavigation.SerialNumber;
                            worksheet.Cells[7][startRowIndex] = allOrders[i].IdClientNavigation.Sname;
                            worksheet.Cells[8][startRowIndex] = allOrders[i].IdClientNavigation.Fname;
                            worksheet.Cells[9][startRowIndex] = allOrders[i].IdClientNavigation.Lname;
                            worksheet.Cells[10][startRowIndex] = allOrders[i].IdClientNavigation.PhoneNumber;
                            worksheet.Cells[11][startRowIndex] = allOrders[i].IdClientNavigation.Address;
                            worksheet.Cells[12][startRowIndex] = allOrders[i].IdClientNavigation.Email;
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                        i++;
                        startRowIndex++;
                    }

                    worksheet.Columns.AutoFit();
                    helper.Save();
                }
                application.Visible = true;
            }
        }
    }

    private void AddUserImpl(Window obj)
    {
        AddUserView auv = new AddUserView();
        auv.Show();
        obj.Close();
    }

    private void OpenProfileWindow(Window obj)
    {
        ProfileView pv = new ProfileView();
        pv.Show();
        obj.Close();
    }

    private void OpenRepairWindowImpl(Window obj)
    {
        NewRepairView nrv = new NewRepairView();
        nrv.Show();
        obj.Close();
    }

    public void OpenInfoRepairWindowImpl(Repair rep, Window obj)
    {
        _SelectRepair = rep;
        RepairInfoView riv = new RepairInfoView();
        riv.Show();
        obj.Close();
    }
}