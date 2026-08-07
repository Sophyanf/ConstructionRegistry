using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;

namespace ConstructionRegistry.ViewModels
{
    public class ContractCustomerAddVM : BaseViewModel
    {
        private readonly IContractCustomerService _contractCustomerService;
        private readonly IKontragentService _kontragentService;

        public ActionCommand AddNewContractCustomer { get; }

        #region Properties

        private string _contractCustomerNumber = string.Empty;
        public string ContractCustomerNumber
        {
            get => _contractCustomerNumber;
            set => UpdateValue(ref _contractCustomerNumber, value);
        }

        private DateTime _contractCustomerData = DateTime.Today;
        public DateTime ContractCustomerData
        {
            get => _contractCustomerData;
            set => UpdateValue(ref _contractCustomerData, value);
        }

        // Контрагент, к которому привязывается договор
        private Kontragent? _customer;
        public Kontragent? Customer
        {
            get => _customer;
            set => UpdateValue(ref _customer, value);
        }
        #endregion

        public ContractCustomerAddVM(
            IContractCustomerService contractCustomerService,
            IKontragentService kontragentService)
        {
            _contractCustomerService = contractCustomerService;
            _kontragentService = kontragentService;

            AddNewContractCustomer = new ActionCommand(_ => AddContractCustomerAsync());
        }

        private async void AddContractCustomerAsync()
        {
            // Валидация
            if (Customer == null)
            {
                MessageBox.Show("Выберите контрагента.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ContractCustomerNumber))
            {
                MessageBox.Show("Введите номер договора.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var contractCustomer = new ContractCustomer
            {
                Kontragent = Customer,
                ContractCustomerNumber = ContractCustomerNumber,
                ContractCustomerData = ContractCustomerData
            };

            try
            {
                await _contractCustomerService.AddAsync(contractCustomer);
                MessageBox.Show("Договор успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseCurrentWindow();
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null)
                    errorMsg += $"\nДетали: {ex.InnerException.Message}";

                MessageBox.Show($"Ошибка при сохранении договора: {errorMsg}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseCurrentWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            activeWindow?.Close();
        }
    }
}
