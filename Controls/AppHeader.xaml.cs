using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace ConstructionRegistry.Controls
{
    public partial class AppHeader : Window
    {
        public AppHeader()
        {
            InitializeComponent();
        }
        // Заголовок окна, который будет отображаться в центре шапки
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AppHeader), new PropertyMetadata(""));
        // Методы кнопок работают с окном, в котором находится этот контрол
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null) window.WindowState = WindowState.Minimized;
        }
        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window == null) return;
            window.WindowState = window.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window?.Close();
        }
        // Перетаскивание окна
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                Window.GetWindow(this)?.DragMove();
        }
    }
}