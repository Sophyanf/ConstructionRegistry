using System.Windows;

namespace ConstructionRegistry.Controls
{
    public static class WindowActions
    {
        public static void DragMove(UIElement element)
        {
            var window = Window.GetWindow(element);
            if (window != null)
                window.DragMove(); // Вызываем DragMove у окна, а не у элемента
        }

        public static void Minimize(UIElement element)
        {
            var window = Window.GetWindow(element);
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }

        public static void Maximize(UIElement element)
        {
            var window = Window.GetWindow(element);
            if (window == null) return;

            window.WindowState = (window.WindowState == WindowState.Maximized)
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        public static void Close(UIElement element)
        {
            var window = Window.GetWindow(element);
            if (window != null)
                window.Close();
        }
    }
}
