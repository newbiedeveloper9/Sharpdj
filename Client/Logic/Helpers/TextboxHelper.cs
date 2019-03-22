using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SharpDj.Logic.Helpers
{
    public class TextboxHelper
    {
        public static void CreateNewLine(TextBox txtBox)
        {
            var caret = txtBox.CaretIndex;
            txtBox.Text = txtBox.Text.Insert(caret, Environment.NewLine);
            txtBox.CaretIndex = caret + Environment.NewLine.Length;
        }
    }
}
