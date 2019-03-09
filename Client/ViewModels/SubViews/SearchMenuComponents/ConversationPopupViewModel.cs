using Caliburn.Micro;
using SCPackets;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using System;
using System.Windows.Media;

namespace SharpDj.ViewModels.SubViews.SearchMenuComponents
{
    public class ConversationPopupViewModel : PropertyChangedBase
    {
        public MessageBindable<MessageModel> MessagesCollection { get; private set; }
        private SolidColorBrush _color;
        public  SolidColorBrush Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                NotifyOfPropertyChange(() => Color);
                foreach (var messageModel in MessagesCollection)
                {
                    messageModel.Color = value;
                }
            }
        }

        public ConversationPopupViewModel()
        {
            var author = new UserClient(1, "Test Diggins", Rank.Moderator);

            MessagesCollection = new MessageBindable<MessageModel>()
            {
                new MessageModel()
                    {Author = author, Id = 0, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel()
                {
                    Author = UserInfoSingleton.Instance.UserClient, Id = 1, Text = "Testowy przykład wiadomości",
                    Time = DateTime.UtcNow
                },
                new MessageModel()
                {
                    Author = UserInfoSingleton.Instance.UserClient, Id = 2, Text = "Testowy przykład wiadomości",
                    Time = DateTime.UtcNow
                },
                new MessageModel()
                    {Author = author, Id = 3, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel() {Author = author, Id = 4, Text = "Testowy przykład", Time = DateTime.UtcNow},
                new MessageModel()
                {
                    Author = UserInfoSingleton.Instance.UserClient, Id = 5, Text = "Testowy przykład wiadomości",
                    Time = DateTime.UtcNow
                },
            };
        }

        public void Minimize()
        {

        }

        public class MessageBindable<T> : BindableCollection<Models.MessageModel>
        {
            protected override void InsertItemBase(int index, Models.MessageModel item)
            {
                if (!(item is Models.MessageModel model)) return;

                if (Count > 0)
                {
                    MessageModel lastMess = Items[Count - 1];
                    if (lastMess.Author.Equals(model.Author))
                    {
                        lastMess.Separator = false;
                    }
                }
                item.Separator = true;

                base.InsertItemBase(index, item);
            }
        }
    }
}
