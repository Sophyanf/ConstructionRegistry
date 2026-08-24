// ViewModels/MainWindowVM.cs
using ConstructionRegistry.Data;
using ConstructionRegistry.Services;
using ConstructionRegistry.Services.Impl;
using ConstructionRegistry.Views;
using System.Windows;
using System.Windows.Input;

namespace ConstructionRegistry.ViewModels
{
    public class MainWindowVM
    {
        public object MainViewInstance { get; }

        public ICommand CloseWindowCommand { get; }
        public ICommand WindowMinimizeCommand { get; }
        public ICommand WindowMaximizeCommand { get; }

        public MainWindowVM(AppDbContext context)
        {
            // Создаём сервисы, передавая им один и тот же контекст
            var objectService = new ConstructionObjectService(context);
            var personService = new ResponsiblPersonService(context);

            // Создаём MainViewVM с этими сервисами
            var mainViewVM = new MainViewVM(objectService, personService);

            // Устанавливаем MainView как контент
            MainViewInstance = new MainView { DataContext = mainViewVM };

            // Команды управления окном
            CloseWindowCommand = new ActionCommand(_ => Application.Current.MainWindow?.Close());
            WindowMinimizeCommand = new ActionCommand(_ => Application.Current.MainWindow.WindowState = WindowState.Minimized);
            WindowMaximizeCommand = new ActionCommand(_ =>
            {
                var window = Application.Current.MainWindow;
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            });
        }
    }
}
