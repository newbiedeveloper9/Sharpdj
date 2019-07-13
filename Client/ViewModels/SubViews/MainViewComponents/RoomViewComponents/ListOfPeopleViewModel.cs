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
        IHandle<IRoomUserListBufferPublish>
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
            UsersCollection.Add(new UserClientModel(0, "Jeff Diggins", Rank.User));
            UsersCollection.Add(new UserClientModel(0, "zonk256", Rank.Admin));
            UsersCollection.Add(new UserClientModel(0, "Testtt", Rank.Moderator));

        }
        #endregion .ctor

        public void Handle(IRoomUserListBufferPublish message)
        {
            UsersCollection.AddRange(message.UsersBuffer.InsertUserList);

            foreach (var updateClient in message.UsersBuffer.UpdateUserList)
            {
                var tmp = UsersCollection.FirstOrDefault(x => x.Id == updateClient.Id);
                if (tmp == null) continue;

                tmp.Username = updateClient.Username;
                tmp.RankTmp = updateClient.RankTmp;
            }

            UsersCollection.RemoveRange(message.UsersBuffer.RemoveUserList);
        }
    }
}
