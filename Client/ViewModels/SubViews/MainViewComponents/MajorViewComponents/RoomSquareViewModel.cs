﻿using System;
using Caliburn.Micro;
using SharpDj.Models;
using SharpDj.PubSubModels;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents
{
    public class RoomSquareViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        public BindableCollection<RoomModel> RoomInstancesCollection { get; private set; }

        public RoomSquareViewModel()
        {
            
        }

        public RoomSquareViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.PublishOnUIThread(new object());

            var current = new TrackModel() { Name = "Hashinshin VS NASUS (and Tanks) - Streamhighlights" };
            var next = new TrackModel() { Name = "jfarr & Willford - Blue Eyes (feat. Hanna Pettersson) | Ninety9Lives Release"};
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

        public void OpenRoom()
        {
            _eventAggregator.PublishOnUIThread(new RoomInfoForOpen());
        }
    }
}