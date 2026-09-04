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
            try
            {
                InitializeComponent();
                var sp = App.ServiceProvider;
                var responsiblPersonService = sp.GetRequiredService<IResponsiblPersonService>();
                var kontragentService = sp.GetRequiredService<IKontragentService>();
                DataContext = new ResponsiblPersonVM(responsiblPersonService, kontragentService);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
