using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents
{
    public class RoomSquareViewModel : PropertyChangedBase,
        IHandle<IRefreshOutsideRoomsPublish>,
        IHandle<IUpdateOutsideRoomPublish>,
        IHandle<INewRoomCreatedPublish>
    {
        private readonly IEventAggregator _eventAggregator;

        private BindableCollection<RoomModel> _roomInstancesCollection = new BindableCollection<RoomModel>();
        public BindableCollection<RoomModel> RoomInstancesCollection
        {
            get => _roomInstancesCollection;
            set
            {
                if (_roomInstancesCollection == value) return;
                _roomInstancesCollection = value;
                NotifyOfPropertyChange(() => RoomInstancesCollection);
            }
        }


        public RoomSquareViewModel()
        {
            var current = new TrackModel() { Name = "Hashinshin VS NASUS (and Tanks) - Streamhighlights" };
            var next = new TrackModel() { Name = "jfarr & Willford - Blue Eyes (feat. Hanna Pettersson) | Ninety9Lives Release" };
            var previous = new TrackModel() { Name = "Finesu - Homecoming (feat. jfarr) | Ninety9Lives Release" };
            var dicPic = @"..\..\..\..\Images\3.jpg";

            RoomInstancesCollection = new BindableCollection<RoomModel>()
            {
                new RoomModel(){
                    AmountOfAdministration = 3,
                    AmountOfPeople = 22,
                    CurrentTrack = current,
                    NextTrack = next,
                    PreviousTrack = previous,
                    Name = "Test room name",
                    Id = 3,
                    ImageSource = dicPic
                },
                new RoomModel(){
                    AmountOfAdministration = 2,
                    AmountOfPeople = 91,
                    CurrentTrack = previous,
                    NextTrack = current,
                    PreviousTrack = next,
                    Name = "12345678901234567890123456789012345678901234567890",
                    Id = 2,
                    ImageSource = dicPic
                }
            };
        }

        public RoomSquareViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void OpenRoom()
        {
            _eventAggregator.PublishOnUIThread(new RoomInfoForOpen());
        }

        public void Handle(IRefreshOutsideRoomsPublish message)
        {
            RoomInstancesCollection.Clear();

            if (message?.Rooms == null) return;
            foreach (var room in message.Rooms)
            {
                RoomInstancesCollection.Add(RoomModel.ToClientModel(room));
            }

        }
        public void Handle(INewRoomCreatedPublish message)
        {
            if (message.RoomOutsideModel == null) return;

            RoomInstancesCollection.Add(
                RoomModel.ToClientModel(message.RoomOutsideModel));
        }

        public void Handle(IUpdateOutsideRoomPublish message)
        {
            for (int i = 0; i < RoomInstancesCollection.Count; i++)
            {
                if(RoomInstancesCollection[i].Id == message.RoomOutsideModel.Id)
                    RoomInstancesCollection[i] = RoomModel.ToClientModel(message.RoomOutsideModel);
            }
        }
    }
}
