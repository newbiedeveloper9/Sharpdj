using System;
using Caliburn.Micro;
using Communication.Client;
using Communication.Client.User;
using Communication.Shared;
using Communication.Shared.Data;
using SharpDj.Logic.Helpers;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.SearchMenuComponents
{
    public class ConversationPopupViewModel : PropertyChangedBase
    {
        public BindableCollection<MessageListModel> RightMessagesCollection { get; private set; }
        public BindableCollection<MessageListModel> LeftMessagesCollection { get; private set; }


        public ConversationPopupViewModel()
        {
            var author = new UserClient(1, "Test Diggins", Rank.Moderator);
            LeftMessagesCollection = new BindableCollection<MessageListModel>();
            RightMessagesCollection = new BindableCollection<MessageListModel>();


            /*MessageCollection = new BindableCollection<MessageModel>()
            {
                
                /*new MessageModel(){Author = author, Id = 0, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = UserInfoSingleton.Instance.UserClient, Id = 1, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = UserInfoSingleton.Instance.UserClient, Id = 2, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = author, Id = 3, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},
                new MessageModel(){Author = author, Id = 4, Text = "Testowy przykład", Time = DateTime.UtcNow},
                new MessageModel(){Author = UserInfoSingleton.Instance.UserClient, Id = 5, Text = "Testowy przykład wiadomości", Time = DateTime.UtcNow},#1#
            };*/
        }

        public void Minimize()
        {

        }
    }
}
