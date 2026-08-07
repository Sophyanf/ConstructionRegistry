using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConstructionRegistry.Views
{
    public partial class AddKontragentView : BaseWindowView
    {
        public AddKontragentView()
        {
            InitializeComponent();

            var serviceProvider = App.ServiceProvider;

            // Берём только один сервис — этого достаточно
            var kontragentService = serviceProvider.GetRequiredService<IKontragentService>();

            // Передаём его в VM (конструктор теперь принимает один параметр)
            var vm = new KontragentAddVM(kontragentService);
            DataContext = vm;
        }
    }
}
