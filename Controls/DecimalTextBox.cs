using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConstructionRegistry.Controls
{
    public class DecimalTextBox : TextBox
    {
        private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            string proposedText = GetProposedText(e.Text);
            if (!IsValidNumber(proposedText))
                e.Handled = true;
        }

      

        private string GetProposedText(string input)
        {
            int start = SelectionStart;
            int length = SelectionLength;
            return Text.Remove(start, length).Insert(start, input);
        }

        private string GetResultAfterPaste(string pasted)
        {
            int start = SelectionStart;
            int length = SelectionLength;
            return Text.Remove(start, length).Insert(start, pasted);
        }

        private bool IsValidNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            return decimal.TryParse(text, System.Globalization.NumberStyles.Number, _culture, out _);
        }
    }
}
