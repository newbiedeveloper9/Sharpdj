using System;
using System.Threading.Tasks;
using System.Windows;

namespace SharpDj.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SdjMainView : Window
    {
        public SdjMainView()
        {
            DataContext = new ViewModel.SdjMainViewModel();
            InitializeComponent();
        }
    }
}
