using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Podchody
{
    public static class AddControl
    {
        public static Label addLabel(int width, int height, int leftMargin, int topMargin, int rightMargin, int buttomMargin, string content)
        {
            Label label = new Label();
            label = (Label) propertiesControl(label, width, height, leftMargin, topMargin, rightMargin, buttomMargin);
            label.Content = content;
            return label;
        }
        public static TextBox addTextBox(int width, int height, int leftMargin, int topMargin, int rightMargin, int buttomMargin, string text)
        {
            TextBox textBox = new TextBox();
            textBox = (TextBox) propertiesControl(textBox, width, height, leftMargin, topMargin, rightMargin, buttomMargin);
            textBox.Text = text;
            return textBox;
        }
        public static CheckBox addCheckBox(int width, int height, int leftMargin, int topMargin, int rightMargin, int buttomMargin, string text)
        {
            CheckBox checkBox = new CheckBox();
            checkBox = (CheckBox) propertiesControl(checkBox, width, height, leftMargin, topMargin, rightMargin, buttomMargin);
            checkBox.Content = text;
            return checkBox;
        }

        public static CheckBox addCheckBox(int leftMargin, int topMargin, int rightMargin, int buttomMargin)
        {
            CheckBox checkBox = new CheckBox();
            checkBox = (CheckBox) propertiesControl(checkBox, 15, 15, leftMargin, topMargin, rightMargin, buttomMargin);
            return checkBox;
        }
        private static Control propertiesControl(Control control, int width, int height, int leftMargin, int topMargin, int rightMargin, int buttomMargin)
        {
            control.Width = width;
            control.Height = height;
            if (leftMargin == 0)
            {
                control.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            }
            else
            {
                control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                rightMargin = 0;
            }
            if (topMargin == 0)
            {
                control.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            }
            else
            {
                control.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                buttomMargin = 0;
            }
            control.Margin = new System.Windows.Thickness(leftMargin, topMargin, rightMargin, buttomMargin);
            return control;
        }
    }
}
