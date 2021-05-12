using System.Linq;
using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.RoomViewComponents
{
    public class ListOfPeopleViewModel : PropertyChangedBase,
        IHandle<IRoomUserListBufferPublish>, IHandle<IRoomInfoForOpen>
    {
        #region _fields
        public readonly IEventAggregator _eventAggregator;
        #endregion _fields

        #region Properties
        private BindableCollection<UserClient> _usersCollectionCollection = new BindableCollection<UserClient>();
        public BindableCollection<UserClient> UsersCollection
        {
            get => _usersCollectionCollection;
            set
            {
                if (_usersCollectionCollection == value) return;
                _usersCollectionCollection = value;
                NotifyOfPropertyChange(() => UsersCollection);
            }
        }

        #endregion Properties

        #region .ctor
        public ListOfPeopleViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);


        }

        public ListOfPeopleViewModel()
        {
            UsersCollection.Add(new UserClient(0, "Crisey"/*, Rank.Admin*/));
            UsersCollection.Add(new UserClient(1, "Jeff Diggins"/*, Rank.User*/));
            UsersCollection.Add(new UserClient(2, "zonk256"/*, Rank.Admin*/));
            UsersCollection.Add(new UserClient(3, "Testtt"/*, Rank.Moderator*/));

        }
        #endregion .ctor

        public void Handle(IRoomUserListBufferPublish message)
        {
            foreach (var user in message.UsersBuffer.InsertUsers.ToReadonlyList())
            {
                if (!UsersCollection.Any(x => x.Id == user.Id))
                {
                    UsersCollection.Add(user);
                }
            }

            foreach (var user in message.UsersBuffer.UpdateUsers)
            {
                var tmp = UsersCollection.FirstOrDefault(x => x.Id == user.Id);
                if (tmp == null) continue;

                tmp.Username = user.Username;
             //   tmp.Rank = user.Rank;
            }

            foreach (var user in message.UsersBuffer.RemoveUsers.ToReadonlyList())
            {
                var tmp = UsersCollection.FirstOrDefault(x => x.Id == user.Id);
                if (tmp != null)
                    UsersCollection.Remove(tmp);
            }
        }

        public void Handle(IRoomInfoForOpen message)
        {
            if (message.UserList != null)
                UsersCollection = new BindableCollection<UserClient>(message.UserList);
        }
    }
}
