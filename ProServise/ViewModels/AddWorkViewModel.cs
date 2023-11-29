using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ProServise.Models;
using ProServise.Views;
using ReactiveUI;

namespace ProServise.ViewModels;

public class AddWorkViewModel : ViewModelBase
{
    private Work _newWork = new Work();
    private string _nameWork;
    private float _totalPriceWork;
    private int _discountWork;
    
    public string NameWork
    {
        get => _nameWork;
        set => this.RaiseAndSetIfChanged(ref _nameWork, value);
    }

    public float TotalPriceWork
    {
        get => _totalPriceWork;
        set
        {
            this.RaiseAndSetIfChanged(ref _totalPriceWork, value);
        }
    }

    public int DiscountWork
    {
        get => _discountWork;
        set => this.RaiseAndSetIfChanged(ref _discountWork, value);
    }
    
    public ReactiveCommand<Window, Unit> AddWork { get; }
    public AddWorkViewModel()
    {
        AddWork = ReactiveCommand.Create<Window>(AddWorkImpl);
    }
    
    private void AddWorkImpl(Window obj)
    {
        var work = Helper.GetContext().Works.FirstOrDefault(x => x.Name == NameWork);

        if (work == null)
        {
            _newWork.Name = NameWork;
            _newWork.TotalPrice = _totalPriceWork;
            _newWork.Discount = _discountWork;
            Helper.GetContext().Works.AddAsync(_newWork);
            Helper.GetContext().SaveChanges();
            RepairInfoView riv = new RepairInfoView();
            riv.Show();
            obj.Close();
            MessageBoxManager.GetMessageBoxStandard("Успех", "Работа добавлена", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Работа уже существует", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }
}