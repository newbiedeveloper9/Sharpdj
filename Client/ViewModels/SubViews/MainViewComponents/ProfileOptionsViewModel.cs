using System;
using Caliburn.Micro;
using SCPackets;
using SharpDj.Enums;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class ProfileOptionsViewModel : PropertyChangedBase, 
        IHandle<ILoginPublish>
    {
        private readonly IEventAggregator _eventAggregator;

        private Rank _role;
        public Rank Role
        {
            get => _role;
            set
            {
                if (_role == value) return;
                _role = value;
                NotifyOfPropertyChange(() => Role);
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }


        public ProfileOptionsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
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
                case "ManageRooms":
                    _eventAggregator.PublishOnUIThread(NavigateMainView.ManageRooms);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException("path");
            }
        }

        public void Handle(ILoginPublish message)
        {
            Role = message.Client.Rank;
            Username = message.Client.Username;
        }
    }
}
