using Caliburn.Micro;
using System.Windows;
using static System.Windows.Application;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class TopMenuViewModel : PropertyChangedBase
    {
        public TopMenuViewModel()
        {
        }

        /* Just in case: https://stackoverflow.com/a/35945363 */
        public void DragWindow()
        {
            Current.MainWindow?.DragMove();
        }

        public void MinimizeApplication()
        {
            if (Current.MainWindow == null) return;

            Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #region MaximizeMethods
        private Point _windowSize = new Point(0, 0);
        private Point _windowPosition = new Point(0, 0);

        private void ShrinkWindow()
        {
            if (Current.MainWindow == null) return;

            Current.MainWindow.WindowState = WindowState.Normal;
            _windowSize.X = Current.MainWindow.Width;
            _windowSize.Y = Current.MainWindow.Height;
            _windowPosition.X = Current.MainWindow.Left;
            _windowPosition.Y = Current.MainWindow.Top;

            Current.MainWindow.Height = SystemParameters.WorkArea.Height;
            Current.MainWindow.Width = SystemParameters.WorkArea.Width;
            Current.MainWindow.Left = 0;
            Current.MainWindow.Top = 0;
        }

        private void ExpandWindow()
        {
            if (Current.MainWindow == null) return;

            Current.MainWindow.WindowState = WindowState.Normal;
            Current.MainWindow.Width = _windowSize.X;
            Current.MainWindow.Height = _windowSize.Y;
            Current.MainWindow.Left = _windowPosition.X;
            Current.MainWindow.Top = _windowPosition.Y;
        }
        #endregion MaximizeMethods

        public void MaximizeApplication()
        {
            if (Current.MainWindow?.Height >= SystemParameters.WorkArea.Height &&
                Current.MainWindow.Width >= SystemParameters.WorkArea.Width)
                ExpandWindow();
            else
                ShrinkWindow();
        }

        public void CloseApplication()
        {
            Current.Shutdown(0);
        }
    }
}
