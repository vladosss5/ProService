using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class AddSpareViewModel : ViewModelBase
{
    private string _nameSpare;
    private float _purchasePriseSpare;
    private float _retailPriseSpare;
    private int _countSpare;
    private Spare _newSpare = new Spare();
    public string NameSpare
    {
        get => _nameSpare;
        set => this.RaiseAndSetIfChanged(ref _nameSpare, value);
    }

    public float PurchasePrice
    {
        get => _purchasePriseSpare;
        set => this.RaiseAndSetIfChanged(ref _purchasePriseSpare, value);
    }

    public float RetailPrice
    {
        get => _retailPriseSpare;
        set => this.RaiseAndSetIfChanged(ref _retailPriseSpare, value);
    }

    public int CountSpare
    {
        get => _countSpare;
        set => this.RaiseAndSetIfChanged(ref _countSpare, value);
    }
    public ReactiveCommand<Window, Unit> AddSpare { get; }
    public AddSpareViewModel()
    {
        AddSpare = ReactiveCommand.Create<Window>(AddSpareImpl);
    }

    private void AddSpareImpl(Window obj)
    {
        var spare = Helper.GetContext().Spares.FirstOrDefault(x => x.Name == NameSpare);

        if (spare == null)
        {
            _newSpare.Name = NameSpare;
            _newSpare.CountSpare = CountSpare;
            _newSpare.PurchasePrice = PurchasePrice;
            _newSpare.RetailPrice = RetailPrice;
            Helper.GetContext().Spares.Add(_newSpare);
            Helper.GetContext().SaveChanges();

            RepairInfoView rpv = new RepairInfoView();
            rpv.Show();
            obj.Close();
            
            MessageBoxManager.GetMessageBoxStandard("Успех", "Запчасть добавлена", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Запчасть уже существует", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }
}