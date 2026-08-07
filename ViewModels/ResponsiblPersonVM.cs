using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;

namespace ConstructionRegistry.ViewModels
{
    public class ResponsiblPersonVM : BaseViewModel
    {
        private readonly IResponsiblPersonService _personService;
        private readonly IKontragentService _kontragentService;

        public ActionCommand AddNewResponsiblPerson { get; }

        #region Properties
        // --- Поля формы ---
        private string _personFIO = string.Empty;
        public string PersonFIO
        {
            get => _personFIO;
            set => UpdateValue(ref _personFIO, value);
        }

        private string _personPost = string.Empty;
        public string PersonPost
        {
            get => _personPost;
            set => UpdateValue(ref _personPost, value);
        }

        private string _personDocument = string.Empty;
        public string PersonDocument
        {
            get => _personDocument;
            set => UpdateValue(ref _personDocument, value);
        }

        private string _personFunctions = string.Empty;
        public string PersonFunctions
        {
            get => _personFunctions;
            set => UpdateValue(ref _personFunctions, value);
        }

        // --- Выбор контрагента ---
        // Лучше хранить ID, а не весь объект, для чистоты данных
        private int _selectedKontragentId;
        public int SelectedKontragentId
        {
            get => _selectedKontragentId;
            set => UpdateValue(ref _selectedKontragentId, value);
        }

        // Объект нужен только для отображения имени в ComboBox (DisplayMemberPath)
        private Kontragent? _selectedKontragentObj;
        public Kontragent? SelectedKontragent
        {
            get => _selectedKontragentObj;
            set
            {
                UpdateValue(ref _selectedKontragentObj, value);
                // При выборе объекта сразу обновляем ID
                SelectedKontragentId = value?.ID ?? 0;
            }
        }

        // Коллекция для привязки
        public ObservableCollection<Kontragent> Kontragents { get; } = new();

        #endregion

        public ResponsiblPersonVM(
            IResponsiblPersonService personService,
            IKontragentService kontragentService)
        {
            _personService = personService;
            _kontragentService = kontragentService;

            AddNewResponsiblPerson = new ActionCommand(_ => AddNewPersonAsync());

            // Инициализация загрузки
            _ = LoadKontragentsAsync();
        }

        private async Task LoadKontragentsAsync()
        {
            try
            {
                var list = await _kontragentService.GetAllAsync();
                Kontragents.Clear();
                foreach (var item in list)
                {
                    Kontragents.Add(item);
                }

                // Опционально: выбрать первого контрагента по умолчанию, если список не пуст
                if (Kontragents.Count > 0)
                {
                    SelectedKontragent = Kontragents[0];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки контрагентов: {ex}"); // Логирование вместо спама MessageBox
                MessageBox.Show("Не удалось загрузить список контрагентов. Проверьте соединение с БД.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddNewPersonAsync()
        {
            // Проверка через ID надежнее
            if (SelectedKontragentId <= 0)
            {
                MessageBox.Show("Пожалуйста, выберите контрагента.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PersonFIO))
            {
                MessageBox.Show("Поле «ФИО» обязательно для заполнения.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var person = new ResponsiblPerson
            {
                PersonFIO = PersonFIO,
                PersonPost = PersonPost,
                PersonDocument = PersonDocument,
                PersonKontragent = SelectedKontragent

            };

            try
            {
                await _personService.AddAsync(person);
                MessageBox.Show("Ответственное лицо успешно добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                // Специфическая обработка ошибок БД (например, дубликат ИНН или нарушение FK)
                string errorMessage = ex.Message;
                if (ex.InnerException != null) errorMessage += $"\nДетали: {ex.InnerException.Message}";

                MessageBox.Show($"Ошибка при сохранении: {errorMessage}", "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseCurrentWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            activeWindow?.Close();
        }
    }
}
