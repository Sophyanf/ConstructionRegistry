using System.Windows;
using System.Windows.Input;

namespace ConstructionRegistry.Views
{
    public partial class AddObjectView : Window
    {
        public AddObjectView()
        {
            InitializeComponent();
        }

        // Теперь это работает, потому что класс — Window, у него есть DragMove()
        public void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

      
    }
}