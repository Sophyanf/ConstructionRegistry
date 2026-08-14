using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConstructionRegistry.Views
{
    public partial class AddResponsiblPerson : Window
    {
        public AddResponsiblPerson()
        {
            InitializeComponent();

            var sp = App.ServiceProvider;

            // Получаем сервисы ТОЧНО в порядке, который требует конструктор VM
            var responsiblPersonService = sp.GetRequiredService<IResponsiblPersonService>();
            var kontragentService = sp.GetRequiredService<IKontragentService>();

            // Создаём VM с правильными аргументами
            var vm = new ResponsiblPersonVM(
                responsiblPersonService,
                kontragentService
            );

            DataContext = vm;
        }
    }
}
