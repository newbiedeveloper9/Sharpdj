using System.Windows;
using SharpDj.Logic.Helpers;
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
            var onStart = new ExecuteOnStart();
            DataContext = new SdjMainViewModel();
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));            
        }
    }
}