using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SharpDj.Logic.UI
{
    class ScrollViewerLogic
    {
        public ScrollViewer ScrollViewer { get; private set; }
        public bool AutoScroll { get; set; } = true;
        public bool CanScrollDown { get; private set; } = true;

        public ScrollViewerLogic(ScrollViewer scrollViewer)
        {
            ScrollViewer = scrollViewer;
            ScrollViewer.ScrollChanged += ScrollViewerOnScrollChanged;
        }

        private void ScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!AutoScroll) return;

            if (e.ExtentHeightChange == 0)
            {
                CanScrollDown = ScrollViewer.VerticalOffset == ScrollViewer.ScrollableHeight;
                OnScrollNotOnBottom(EventArgs.Empty);
            }

            if (CanScrollDown && e.ExtentHeightChange != 0)
                ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ExtentHeight);
        }

        public void ScrollToDown()
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ExtentHeight);
        }


        public event EventHandler ScrollNotOnBottom;
        protected virtual void OnScrollNotOnBottom(EventArgs e)
        {
            var handler = ScrollNotOnBottom;
            handler?.Invoke(this, e);
        }
    }
}
