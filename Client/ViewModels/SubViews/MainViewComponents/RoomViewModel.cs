using System;
using Caliburn.Micro;
using SharpDj.Logic.UI;
using SharpDj.Models;
using SharpDj.Views.SubViews.MainViewComponents;
using System.Windows.Controls;
using SharpDj.Interfaces;

namespace SharpDj.ViewModels.SubViews.MainViewComponents
{
    public class RoomViewModel : PropertyChangedBase,
        INavMainView
    {
        private ScrollViewerLogic scrollViewerLogic;
        public BindableCollection<CommentModel> CommentsCollection { get; private set; }

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

        public void SendChatMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText)) return;

            CommentsCollection.Add(new CommentModel() { Author = "Crisey", Comment = MessageText });
            MessageText = string.Empty;
        }


        private bool _scrollToBottomIsVisible = false;
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
        public void ScrollToBottom()
        {
            scrollViewerLogic.ScrollToDown();
        }

        public void ScrollLoaded(ScrollViewer scrollViewer)
        {
            scrollViewerLogic = new ScrollViewerLogic(scrollViewer);
            scrollViewerLogic.ScrollNotOnBottom +=
                (sender, args) => ScrollToBottomIsVisible = !scrollViewerLogic.CanScrollDown;
        }
    }
}
