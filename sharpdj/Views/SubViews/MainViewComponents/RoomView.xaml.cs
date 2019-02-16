using System;
using System.Windows.Controls;

namespace SharpDj.Views.SubViews.MainViewComponents
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();
        }

        public bool Test
        {
            get => _test;
            set
            {
                _test = value;
                OnCanScrollDown(EventArgs.Empty);
            }
        }

        private bool AutoScroll = true;

        private bool _test = false;
        /*
         * https://stackoverflow.com/a/19315242
         */

        public void ChatScrollDown()
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ExtentHeight);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // User scroll event : set or unset auto-scroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (ScrollViewer.VerticalOffset == ScrollViewer.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set auto-scroll mode
                    AutoScroll = true;
                    Test = false;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset auto-scroll mode
                    AutoScroll = false;
                    Test = true;
                }
            }

            // Content scroll event : auto-scroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and auto-scroll mode set
                // Autoscroll
                ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ExtentHeight);
            }
        }

        public event EventHandler CanScrollDown;

        protected virtual void OnCanScrollDown(EventArgs e)
        {
            var handler = CanScrollDown;
            handler?.Invoke(this, e);
        }
    }
}
