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
        private readonly IWindowNavigator _navigator;  // ← добавили

        public ObservableCollection<ConstructionObject> ObjectsList { get; } = new();
        public ObservableCollection<ResponsiblPerson> PersonsList { get; } = new();

        public ConstructionObject? SelectObject { get; set; }
        public ResponsiblPerson? SelectPerson { get; set; }

        public ActionCommand AddNewObjectDB { get; }
        public ActionCommand AddPersonDB { get; }
        public ActionCommand RemoveObjectDB { get; }

        // Добавили IWindowNavigator в параметры конструктора
        public MainViewVM(
            IConstructionObjectService objectService,
            IResponsiblPersonService personService,
            IWindowNavigator navigator)              // ← вот сюда
        {
            _objectService = objectService;
            _personService = personService;
            _navigator = navigator;                   // ← сохранили

            AddNewObjectDB = new ActionCommand(_ => AddNewObject());
            AddPersonDB = new ActionCommand(_ => AddPerson());
            RemoveObjectDB = new ActionCommand(_ => RemoveObject());

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
            // Было:
            // var addObjectWindow = new AddObjectView();
            // addObjectWindow.ShowDialog();
            // LoadObjectsAsync();

            // Стало:
            if (_navigator.ShowModal<AddObjectView>())
            {
                LoadObjectsAsync();  // Обновляем только если окно вернуло true
            }
        }

        private void AddPerson()
        {
            // Было:
            // var addResponsiblPersonWindow = new Views.AddResponsiblPerson();
            // addResponsiblPersonWindow.ShowDialog();
            // LoadPersonsAsync();

            // Стало:
            if (_navigator.ShowModal<AddResponsiblPerson>())
            {
                LoadPersonsAsync();
            }
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
