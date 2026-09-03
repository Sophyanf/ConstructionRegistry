using System.Windows;

namespace ConstructionRegistry.Services
{
    public interface IWindowNavigator
    {
        bool ShowModal<T>(object dataContext = null) where T : Window, new();
    }
}