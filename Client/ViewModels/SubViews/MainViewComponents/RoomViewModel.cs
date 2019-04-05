using System;
using System.Windows;
using Caliburn.Micro;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.Views.SubViews.MainViewComponents;
using System.Windows.Controls;
using SCPackets.Models;
using SharpDj.Enums;
using SharpDj.Interfaces;
using SharpDj.Logic.Helpers;
using SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class RoomViewModel : PropertyChangedBase,
        INavMainView
    {
        #region _fields
        private readonly IEventAggregator _eventAggregator;

        #endregion _fields

        #region Properties
       
        private bool _isSound;
        public bool IsSound
        {
            get => _isSound;
            set
            {
                if (_isSound == value) return;
                _isSound = value;
                NotifyOfPropertyChange(() => IsSound);
            }
        }

        private ChatVisibility _chatVisibility = ChatVisibility.Chat;
        public ChatVisibility ChatVisibility
        {
            get => _chatVisibility;
            set
            {
                if (_chatVisibility == value) return;
                _chatVisibility = value;
                NotifyOfPropertyChange(() => ChatVisibility);
                Console.WriteLine(value);
            }
        }

        private ColorPaletteViewModel _colorPaletteViewModel;
        public ColorPaletteViewModel ColorPaletteViewModel
        {
            get => _colorPaletteViewModel;
            set
            {
                if (_colorPaletteViewModel == value) return;
                _colorPaletteViewModel = value;
                NotifyOfPropertyChange(() => ColorPaletteViewModel);
            }
        }

        private ChatViewModel _chatViewModel;
        public ChatViewModel ChatViewModel
        {
            get => _chatViewModel;
            set
            {
                if (_chatViewModel == value) return;
                _chatViewModel = value;
                NotifyOfPropertyChange(() => ChatViewModel);
            }
        }

        #endregion Properties


        #region .ctor
        public RoomViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ColorPaletteViewModel = new ColorPaletteViewModel(_eventAggregator);
            ChatViewModel = new ChatViewModel(_eventAggregator);
        }

        public RoomViewModel()
        {
            ColorPaletteViewModel = new ColorPaletteViewModel();
            ChatViewModel = new ChatViewModel();
        }

        #endregion .ctor

        #region Methods
       

        public void ChangeSoundMute()
        {
            IsSound = !IsSound;
        }

        public void SetChatVisibility(ChatVisibility type)
        {
            ChatVisibility = type;
        }
        #endregion Methods
    }
}
