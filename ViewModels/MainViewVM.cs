using ConstructionRegistry;
using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using ConstructionRegistry.Views;
using System.Collections.ObjectModel;
using System.Windows;

public class MainViewVM : BaseViewModel
{
    private readonly IConstructionObjectService _objectService;
    private readonly IResponsiblPersonService _personService;
    public ObservableCollection<ConstructionObject> ObjectsList { get; } = new();
    public ObservableCollection<ResponsiblPerson> PersonsList { get; } = new();
    public ConstructionObject? SelectObject { get; set; }
    public ResponsiblPerson? SelectPerson { get; set; }
    public ActionCommand AddNewObjectDB { get; }
    public ActionCommand AddPersonDB { get; }
    public ActionCommand RemoveObjectDB { get; }
    private readonly IWindowNavigator _navigator; // новое поле
    public MainViewVM(
        IConstructionObjectService objectService,
        IResponsiblPersonService personService,
        IWindowNavigator navigator)
    {
        _objectService = objectService;
        _personService = personService;
        _navigator = navigator;
        AddNewObjectDB = new ActionCommand(_ => AddNewObject());
        AddPersonDB = new ActionCommand(_ => AddPerson());
        RemoveObjectDB = new ActionCommand(_ => RemoveObject());
        LoadObjectsAsync();
        LoadPersonsAsync();
    }
    private async Task LoadAllDataAsync()
    {
        await LoadObjectsAsync();
        await LoadPersonsAsync();
    }
    private async Task LoadObjectsAsync() // из void → Task
    {
        try
        {
            var list = await _objectService.GetAllWithCustomerAsync();
            ObjectsList.Clear();
            foreach (var item in list) ObjectsList.Add(item);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}");
        }
    }
    private async Task LoadPersonsAsync() // из void → Task
    {
        try
        {
            var list = await _personService.GetAllByKontragentAsync(0);
            PersonsList.Clear();
            foreach (var item in list) PersonsList.Add(item);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки лиц: {ex.Message}");
        }
    }
    private void AddNewObject()
    {
        if (_navigator.ShowModal<AddObjectView>())
        {
            LoadObjectsAsync();
        }
    }
    private void AddPerson()
    {
        if (_navigator.ShowModal<AddResponsiblPerson>())
        {
            LoadPersonsAsync();
        }
    }
    private async void RemoveObject()
    {
        if (SelectObject == null) return;
        var result = MessageBox.Show(
            $"Удалить объект \"{SelectObject.ObjectName}\"?",
            "Подтверждение", MessageBoxButton.YesNo);
        if (result != MessageBoxResult.Yes) return;
        try
        {
            await _objectService.RemoveAsync(SelectObject.ID);
            LoadObjectsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка удаления: {ex.Message}");
        }
    }
}