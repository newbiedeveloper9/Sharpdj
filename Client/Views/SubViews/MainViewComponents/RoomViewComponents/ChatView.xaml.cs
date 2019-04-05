using System.Windows.Controls;
using System.Windows.Input;
using SharpDj.Logic.Helpers;

namespace SharpDj.Views.SubViews.MainViewComponents.RoomViewComponents
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void ChatMessage_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox txtBox)) return;
            e.Handled = TextboxHelper.ShortcutsFixHandled(txtBox, e.Key);
        }
    }
}
