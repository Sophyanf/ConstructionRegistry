using ConstructionRegistry.Controllers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Window = System.Windows.Window;

namespace ConstructionRegistry.Views {
    /// <summary>
    /// Логика взаимодействия для AddResponsiblPerson.xaml
    /// </summary>
    public partial class AddResponsiblPerson : Window
    {
        //private DataObjectController dataObj = DataObjectController.Instance;
        public AddResponsiblPerson()
        {
            InitializeComponent();

        }


        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
