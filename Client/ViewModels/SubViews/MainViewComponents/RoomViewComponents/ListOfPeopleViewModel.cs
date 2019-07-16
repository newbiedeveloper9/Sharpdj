using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SCPackets.Models;
using SharpDj.Logic.Helpers;
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
        private BindableCollection<UserClientModel> _usersCollectionCollection = new BindableCollection<UserClientModel>();
        public BindableCollection<UserClientModel> UsersCollection
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
            UsersCollection.Add(new UserClientModel(0, "Crisey", Rank.Admin));
            UsersCollection.Add(new UserClientModel(1, "Jeff Diggins", Rank.User));
            UsersCollection.Add(new UserClientModel(2, "zonk256", Rank.Admin));
            UsersCollection.Add(new UserClientModel(3, "Testtt", Rank.Moderator));

        }
        #endregion .ctor

        public void Handle(IRoomUserListBufferPublish message)
        {
            foreach (var user in message.UsersBuffer.InsertUserList)
            {
                // ReSharper disable once SimplifyLinqExpression
                if (!UsersCollection.Any(x => x.Id == user.Id))
                    UsersCollection.Add(user);
            }

            foreach (var user in message.UsersBuffer.UpdateUserList)
            {
                var tmp = UsersCollection.FirstOrDefault(x => x.Id == user.Id);
                if (tmp == null) continue;

                tmp.Username = user.Username;
                tmp.RankTmp = user.RankTmp;
            }

            foreach (var user in message.UsersBuffer.RemoveUserList)
            {
                var tmp = UsersCollection.FirstOrDefault(x => x.Id == user.Id);
                if (tmp != null)
                    UsersCollection.Remove(tmp);
            }
        }

        public void Handle(IRoomInfoForOpen message)
        {
            if (message.UserList != null)
                UsersCollection = new BindableCollection<UserClientModel>(message.UserList);
        }
    }
}
