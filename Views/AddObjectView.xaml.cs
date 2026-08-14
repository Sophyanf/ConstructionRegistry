using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConstructionRegistry.Views
{
    public partial class AddObjectView: Window
    {
        public AddObjectView()
        {
            InitializeComponent();

            var sp = App.ServiceProvider;

            // Получаем сервисы в ТОЧНОМ порядке, как требует конструктор VM
            var responsiblPersonService = sp.GetRequiredService<IResponsiblPersonService>();
            var kontragentService = sp.GetRequiredService<IKontragentService>();

            // Передаём их в том же порядке: сначала IResponsiblPersonService, потом IKontragentService
            var vm = new ResponsiblPersonVM(
                responsiblPersonService,
                kontragentService
            );

            DataContext = vm;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
