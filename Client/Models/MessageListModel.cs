using Caliburn.Micro;
using Communication.Client.User;

namespace SharpDj.Models
{
    public class MessageListModel : PropertyChangedBase
    {
        private BindableCollection<MessageModel> _messageModelCollection = new BindableCollection<MessageModel>();
        public BindableCollection<MessageModel> MessageModelCollection
        {
            get => _messageModelCollection;
            set
            {
                if (_messageModelCollection == value) return;
                _messageModelCollection = value;
                NotifyOfPropertyChange(() => MessageModelCollection);
            }
        }

        private UserClient _author = new UserClient();
        public UserClient Author
        {
            get => _author;
            set
            {
                if (_author == value) return;
                _author = value;
                NotifyOfPropertyChange(() => Author);
            }
        }
    }
}
