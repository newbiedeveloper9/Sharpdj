using System;
using Caliburn.Micro;
using SCPackets;
using SharpDj.Enums;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ProfileOptionsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public Rank Role { get; set; } = Rank.Admin;
        public string Username { get; set; } = "Crisey";

        public ProfileOptionsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public ProfileOptionsViewModel()
        {
            
        }

        public void Navigate(string path)
        {
            switch (path)
            {
                case "CreateRoom":
                    _eventAggregator.PublishOnUIThread(NavigateMainView.CreateRoom);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException("path");
            }
        }
    }
}
