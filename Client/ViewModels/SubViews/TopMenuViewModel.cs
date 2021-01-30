using System.Windows;
using Caliburn.Micro;

namespace SharpDj.ViewModels.SubViews
{
    public class TopMenuViewModel : PropertyChangedBase
    {
        public TopMenuViewModel()
        {
        }

        // https://stackoverflow.com/a/35945363
        public void DragWindow()
        {
            Application.Current.MainWindow?.DragMove();
        }

        public void MinimizeApplication()
        {
            if (Application.Current.MainWindow == null)
            {
                return;
            }

            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #region MaximizeMethods
        private Point _windowSize = new Point(0, 0);
        private Point _windowPosition = new Point(0, 0);

        private void ShrinkWindow()
        {
            if (Application.Current.MainWindow == null)
            {
                return;
            }

            Application.Current.MainWindow.WindowState = WindowState.Normal;
            _windowSize.X = Application.Current.MainWindow.Width;
            _windowSize.Y = Application.Current.MainWindow.Height;
            _windowPosition.X = Application.Current.MainWindow.Left;
            _windowPosition.Y = Application.Current.MainWindow.Top;

            Application.Current.MainWindow.Height = SystemParameters.WorkArea.Height;
            Application.Current.MainWindow.Width = SystemParameters.WorkArea.Width;
            Application.Current.MainWindow.Left = 0;
            Application.Current.MainWindow.Top = 0;
        }

        private void ExpandWindow()
        {
            if (Application.Current.MainWindow == null)
            {
                return;
            }

            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Width = _windowSize.X;
            Application.Current.MainWindow.Height = _windowSize.Y;
            Application.Current.MainWindow.Left = _windowPosition.X;
            Application.Current.MainWindow.Top = _windowPosition.Y;
        }
        #endregion MaximizeMethods

        public void MaximizeApplication()
        {
            if (Application.Current.MainWindow?.Height >= SystemParameters.WorkArea.Height &&
                Application.Current.MainWindow.Width >= SystemParameters.WorkArea.Width)
            {
                ExpandWindow();
            }
            else
            {
                ShrinkWindow();
            }
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown(0);
        }
    }
}
