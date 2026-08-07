using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConstructionRegistry.Views
{
    public partial class AddContractCustomerView : BaseWindowView
    {
        public AddContractCustomerView()
        {
            InitializeComponent();

            // Получаем контейнер из App
            var serviceProvider = App.ServiceProvider;

            // Запрашиваем сервисы (они должны быть зарегистрированы в App.xaml.cs)
            var contractService = serviceProvider.GetRequiredService<IContractCustomerService>();
            var kontragentService = serviceProvider.GetRequiredService<IKontragentService>();

            // Создаём VM и привязываем к окну
            var vm = new ContractCustomerAddVM(contractService, kontragentService);
            DataContext = vm;
        }
    }
}
