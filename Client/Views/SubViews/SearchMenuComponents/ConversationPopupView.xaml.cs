using SharpDj.Logic.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Environment = System.Environment;

namespace SharpDj.Views.SubViews.SearchMenuComponents
{
    /// <summary>
    /// Interaction logic for ConversationPopupView.xaml
    /// </summary>
    public partial class ConversationPopupView : UserControl
    {
        public ConversationPopupView()
        {
            InitializeComponent();
        }

        private void Message_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox txtBox)) return;
            e.Handled = TextboxHelper.ShortcutsFixHandled(txtBox, e.Key);
        }
    }
}
