using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharpDj.Logic.Helpers
{
    public class TextboxHelper
    {
        public static void AppendText(TextBox txtBox, string text)
        {
            txtBox.Select(txtBox.CaretIndex, 0);
            txtBox.SelectedText = text;
            txtBox.CaretIndex += text.Length;
        }

        public static bool ShortcutsFixHandled(TextBox txtBox, Key keyPressed)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                switch (keyPressed)
                {
                    case Key.V:
                        AppendText(txtBox, Clipboard.GetText());
                        return true;
                    case Key.Enter:
                        AppendText(txtBox, Environment.NewLine);
                        return true;
                }

            }
            else if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                switch (keyPressed)
                {
                    case Key.Enter:
                        AppendText(txtBox, Environment.NewLine);
                        return true;
                }
            }

            return false;
        }
    }
}
