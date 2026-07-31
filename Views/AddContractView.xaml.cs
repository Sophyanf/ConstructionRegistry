
using System.Windows.Input;

using Window = System.Windows.Window;

namespace ConstructionRegistry.Views
{
    /// <summary>
    /// Логика взаимодействия для AddKontragentView.xaml
    /// </summary>
    public partial class AddContracttView : Window
    {
        public AddContracttView()
        {
            InitializeComponent();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {


            if (IsTextNumeric(e.Text))
            {
                e.Handled = true; // Пометьте событие как обработанное, чтобы предотвратить ввод символа
            }
    }

    private bool IsTextNumeric(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(text);
        }

       
    }
}
