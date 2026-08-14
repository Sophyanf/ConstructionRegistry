using ConstructionRegistry.Controls; // Твой неймспейс
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ConstructionRegistry.Controls
{
    public partial class HeaderPanel : UserControl
    {
        public HeaderPanel()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Вызываем общую логику перетаскивания
            WindowActions.DragMove(this);
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowActions.Minimize(this);
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowActions.Maximize(this);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            WindowActions.Close(this);
        }
    }
}
