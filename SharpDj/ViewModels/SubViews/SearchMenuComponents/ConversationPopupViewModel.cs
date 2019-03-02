using System;
using Caliburn.Micro;
using Communication.Client;
using Communication.Shared;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.SearchMenuComponents
{
    public class ConversationPopupViewModel : PropertyChangedBase
    {
        public BindableCollection<MessageModel> MessageCollection { get; private set; }

        public ConversationPopupViewModel()
        {
            var author = new UserClient(1, "Test Diggins", Rank.Moderator);

            MessageCollection = new BindableCollection<MessageModel>()
            {
                new MessageModel(){Author = author, Id = 0, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = author, Id = 1, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = author, Id = 2, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = author, Id = 3, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
            };
        }
    }
}
