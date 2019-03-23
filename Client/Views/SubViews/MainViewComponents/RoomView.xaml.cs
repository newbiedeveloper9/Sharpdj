using System;
using System.Windows.Controls;
using System.Windows.Input;
using SharpDj.Logic.Helpers;

namespace SharpDj.Views.SubViews.MainViewComponents
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();
        }


        private void MessageText_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox txtBox)) return;
            e.Handled = TextboxHelper.ShortcutsFixHandled(txtBox, e.Key);
        }
    }
}
