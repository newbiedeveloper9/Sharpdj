using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews
{
    class SearchMenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public SearchMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Home()
        {
            _eventAggregator.PublishOnUIThread(new NavigateToHome());
        }
    }
}
