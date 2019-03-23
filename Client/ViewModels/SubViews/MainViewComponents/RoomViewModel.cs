using System;
using Caliburn.Micro;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.Views.SubViews.MainViewComponents;
using System.Windows.Controls;
using SharpDj.Interfaces;
using SharpDj.Logic.Helpers;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class RoomViewModel : PropertyChangedBase,
        INavMainView
    {
        #region _fields
        private ScrollViewerLogic _scrollViewerLogic;

        #endregion _fields

        #region Properties
        private BindableCollection<CommentModel> _commentsCollection;
        public BindableCollection<CommentModel> CommentsCollection
        {
            get => _commentsCollection;
            set
            {
                if (_commentsCollection == value) return;
                _commentsCollection = value;
                NotifyOfPropertyChange(() => CommentsCollection);
            }
        }

        private bool _scrollToBottomIsVisible;

        public bool ScrollToBottomIsVisible
        {
            get => _scrollToBottomIsVisible;
            set
            {
                if (_scrollToBottomIsVisible == value) return;
                _scrollToBottomIsVisible = value;
                NotifyOfPropertyChange(() => ScrollToBottomIsVisible);
            }
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                if (_messageText == value) return;
                _messageText = value;
                NotifyOfPropertyChange(() => MessageText);
            }
        }
        #endregion Properties


        #region .ctor
        public RoomViewModel()
        {
            CommentsCollection = new BindableCollection<CommentModel>()
            {
                new CommentModel(){Author = "Crisey", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Zonk256", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "Jeff Diggins", Comment = "Testowy tekst XD"},
                new CommentModel(){Author = "XDDDDDDD", Comment = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"},
            };
        }

        #endregion .ctor

        #region Methods
        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText)) return;

            CommentsCollection.Add(new CommentModel() { Author = "Crisey", Comment = MessageText });
            MessageText = string.Empty;
        }

        public void ScrollToBottom()
        {
            _scrollViewerLogic.ScrollToDown();
        }

        public void ScrollLoaded(ScrollViewer scrollViewer)
        {
            _scrollViewerLogic = new ScrollViewerLogic(scrollViewer);
            _scrollViewerLogic.ScrollNotOnBottom +=
                (sender, args) => ScrollToBottomIsVisible = !_scrollViewerLogic.CanScrollDown;
        }
        #endregion Methods
    }
}
