using System.Windows;
using SharpDj.Logic.Helpers;

namespace SharpDj
{
    public partial class App : Application
    {
        public App()
        {
            new ExecuteOnStart();
            InitializeComponent();
        }
    }

}
