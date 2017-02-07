using System.Windows;
using System.Windows.Input;

namespace Podchody
{
    static class Validate
    {
        public static void isTime(string text, TextCompositionEventArgs e)
        {
            switch (text.Length)
            {
                case (0):
                    {
                        if (e.Text[0] < 48 || e.Text[0] > 50)
                            e.Handled = true;
                        break;
                    }
                case (1):
                    {
                        if (!(((text[0] == '0' || text[0] == '1') && (e.Text[0] > 47 && e.Text[0] < 58)) || (text[0] == '2' && (e.Text[0] > 47 && e.Text[0] < 52))))
                            e.Handled = true;
                        break;
                    }
                case (2):
                    {
                        if (e.Text[0] != ':')
                            e.Handled = true;
                        break;
                    }
                case (3):
                    {
                        if (e.Text[0] < 48 || e.Text[0] > 53)
                            e.Handled = true;
                        break;
                    }
                case (4):
                    {
                        isNumber(e);
                        break;
                    }
                default:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        public static void isNumber(TextCompositionEventArgs e)
        {
            int result;

            if(!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        public static void deleteFirstZero(object sender)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox.Text.Length == 2 && textBox.Text[0] == '0')
            {
                textBox.Text = textBox.Text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
        public static void textBoxGotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox.Text == "0")
                textBox.Text = "";
            textBox.SelectionStart = textBox.Text.Length;
        }
        public static void textBoxLostFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            if (textBox.Text == "")
                textBox.Text = "0";
        }
    }
}
