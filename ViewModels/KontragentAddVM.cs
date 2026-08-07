using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using System.Threading.Tasks;
using System.Windows;

namespace ConstructionRegistry.ViewModels
{
    public class KontragentAddVM : BaseViewModel
    {
        private readonly IKontragentService _kontragentService;

        public KontragentAddVM(IKontragentService kontragentService)
        {
            _kontragentService = kontragentService;
            AddNewKontragent = new ActionCommand(async _ => await AddKontragentAsync());
        }

        private string _name = string.Empty;
        public string KontragentName
        {
            get => _name;
            set => UpdateValue(ref _name, value);
        }

        private string _shortName = string.Empty;
        public string KontragentShortName
        {
            get => _shortName;
            set => UpdateValue(ref _shortName, value);
        }

        private string _inn = string.Empty;
        // Валидация: только цифры или пусто
        public string KontragentINN
        {
            get => _inn;
            set
            {
                if (string.IsNullOrEmpty(value) || long.TryParse(value, out _))
                    UpdateValue(ref _inn, value);
            }
        }

        private bool _isVatPayer;
        public bool IsVatPayer
        {
            get => _isVatPayer;
            set => UpdateValue(ref _isVatPayer, value);
        }

        public ActionCommand AddNewKontragent { get; }

        private async Task AddKontragentAsync()
        {
            var newKontragent = new Kontragent
            {
                KontragentName = KontragentName,
                KontragentShortName = KontragentShortName,
                KontragentINN = KontragentINN,

                // Адрес НЕ присваиваем здесь — он будет заполнен позже через сервис парсинга
                KontragentAddress = null,

                NDSRate = IsVatPayer
            };

            try
            {
                await _kontragentService.AddAsync(newKontragent);
                MessageBox.Show("Контрагент успешно добавлен! Адрес будет обработан сервисом парсинга.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Тут можно добавить логику: закрыть окно, очистить поля и т.п.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
