using System.Windows;
using System.Windows.Input;
using ConstructionRegistry.ViewModels; // если нужно, но лучше не тянуть VM сюда

namespace ConstructionRegistry.Views
{
    public class BaseWindowView : Window
    {
        public BaseWindowView()
        {
            // Подписываемся на MouseDown глобально для всего окна
            this.MouseDown += OnWindowMouseDown;
        }

        /// <summary>
        /// Перетаскивание окна за любой элемент (если нет стандартной рамки)
        /// </summary>
        private void OnWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Команда: закрыть окно
        /// Вызывай из VM через ((BaseWindow)Window.GetWindow(this)).Close()
        /// Но лучше сделать RelayCommand в VM, которая просто вызывает CloseWindow()
        /// </summary>
        public void CloseWindow()
        {
            this.Close();
        }

        /// <summary>
        /// Команда: свернуть окно
        /// </summary>
        public void MinimizeWindow()
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Команда: развернуть/восстановить окно
        /// </summary>
        public void MaximizeOrRestoreWindow()
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
    }
}
