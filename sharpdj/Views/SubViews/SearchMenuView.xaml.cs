using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SharpDj.Views.SubViews
{
    /// <summary>
    /// Interaction logic for SearchMenuView.xaml
    /// </summary>
    public partial class SearchMenuView : UserControl
    {
        public SearchMenuView()
        {
            InitializeComponent();
        }

        private void PopupBox_OnOpened(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            if (window == null) return;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(20);

                Dispatcher.Invoke(() =>
                {
                    window.Height++;
                    window.Height--;
                });
            });
        }

    }
}
