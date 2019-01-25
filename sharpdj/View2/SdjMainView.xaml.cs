using System.Windows;
using System.Windows.Media;

namespace SharpDj.View2
{
    /// <summary>
    /// Interaction logic for SdjMainView.xaml
    /// </summary>
    public partial class SdjMainView : Window
    {
        public SdjMainView()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.Fant);

        }
    }
}