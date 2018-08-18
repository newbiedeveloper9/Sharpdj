using System.Windows;
using SharpDj.ViewModel;

namespace SharpDj.View.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SdjMainView : Window
    {
        public SdjMainView()
        {
            
            DataContext = new SdjMainViewModel();
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
        }
    }
}