using ConstructionRegistry.Services;
using ConstructionRegistry.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
namespace ConstructionRegistry.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            var sp = App.ServiceProvider;
            DataContext = new MainViewVM(
                           App.GetService<IConstructionObjectService>(),
                           App.GetService<IResponsiblPersonService>(),
                           App.GetService<IWindowNavigator>()
                       );
        }
    }
}