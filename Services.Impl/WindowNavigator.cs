using System.Windows;

namespace ConstructionRegistry.Services.Impl
{
    public class WindowNavigator : IWindowNavigator
    {
        public bool ShowModal<T>(object dataContext = null) where T : Window, new()
        {
            var window = new T();
            if (dataContext != null)
                window.DataContext = dataContext;

            return window.ShowDialog() == true;
        }
    }
}
