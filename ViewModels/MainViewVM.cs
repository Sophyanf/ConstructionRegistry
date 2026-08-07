using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using ConstructionRegistry.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;

namespace ConstructionRegistry.ViewModels
{
    public class MainViewVM : BaseViewModel
    {
        private readonly IConstructionObjectService _objectService;
        private readonly IResponsiblPersonService _personService;

        // Эти коллекции должны быть публичными свойствами, чтобы биндились в XAML
        public ObservableCollection<ConstructionObject> ObjectsList { get; } = new();
        public ObservableCollection<ResponsiblPerson> PersonsList { get; } = new();

        public ConstructionObject? SelectObject { get; set; } // привязка SelectedItem в DataGrid
        public ResponsiblPerson? SelectPerson { get; set; }

        public ActionCommand AddNewObjectDB { get; }
        public ActionCommand AddPersonDB { get; }
        public ActionCommand RemoveObjectDB { get; }

        public MainViewVM(
            IConstructionObjectService objectService,
            IResponsiblPersonService personService)
        {
            _objectService = objectService;
            _personService = personService;

            AddNewObjectDB = new ActionCommand(_ => AddNewObject());
            AddPersonDB = new ActionCommand(_ => AddPerson());
            RemoveObjectDB = new ActionCommand(_ => RemoveObject());

            // Инициализация коллекций уже сделана выше через инициализатор
            LoadObjectsAsync();
            LoadPersonsAsync();
        }

        private async void LoadObjectsAsync()
        {
            try
            {
                var list = await _objectService.GetAllWithCustomerAsync();
                ObjectsList.Clear();
                foreach (var item in list) ObjectsList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки объектов: {ex.Message}");
            }
        }

        private async void LoadPersonsAsync()
        {
            try
            {
                // Если у сервиса есть метод GetAllAsync — используй его.
                // Здесь я делаю заглушку: если такого метода нет, добавь его в IResponsiblPersonService
                // и реализацию в ResponsiblPersonService аналогично другим сервисам.
                var list = await _personService.GetAllByKontragentAsync(0); // 0 — заглушка, если нужен фильтр
                PersonsList.Clear();
                foreach (var item in list) PersonsList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ответственных лиц: {ex.Message}");
            }
        }

        private void AddNewObject()
        {
            // В MVVM не принято делать new View прямо в ViewModel.
            // Самый простой вариант без отдельного NavigationService — через событие или callback.
            // Для твоего текущего уровня — можно оставить вызов ShowDialog через локальную переменную,
            // но вынести его в отдельный метод или сервис.
            var addObjectWindow = new AddObjectView();
            addObjectWindow.ShowDialog();

            // После закрытия окна нужно обновить список
            LoadObjectsAsync();
        }

        private void AddPerson()
        {
            var addResponsiblPersonWindow = new Views.AddResponsiblPerson();
            addResponsiblPersonWindow.ShowDialog();
            LoadPersonsAsync();
        }

        private async void RemoveObject()
        {
            if (SelectObject == null)
            {
                MessageBox.Show("Выберите объект для удаления.");
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить объект \"{SelectObject.ObjectName}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                await _objectService.RemoveAsync(SelectObject.ID);
                MessageBox.Show("Объект удалён.");
                LoadObjectsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось удалить объект: {ex.Message}");
            }
        }
    }
}
