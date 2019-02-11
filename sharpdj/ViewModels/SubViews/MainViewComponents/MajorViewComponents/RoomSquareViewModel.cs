using System;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.MajorViewComponents
{
    public class RoomSquareViewModel : PropertyChangedBase
    {
        public BindableCollection<RoomModel> RoomInstancesCollection { get; private set; }

        public RoomSquareViewModel()
        {
            var current = new RoomModel.Track() { Name = "Hashinshin VS NASUS (and Tanks) - Streamhighlights" };
            var next = new RoomModel.Track() { Name = "jfarr & Willford - Blue Eyes (feat. Hanna Pettersson) | Ninety9Lives Release" };
            var previous = new RoomModel.Track() { Name = "Finesu - Homecoming (feat. jfarr) | Ninety9Lives Release" };
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
                    Name = "XD xd xd xd Lorem Ipsum dolor xD XD xd",
                    Id = 2,
                    ImageSource = dicPic
                }
            };
        }

        public void OpenRoom()
        {
            //add EventAggregator for set room viewmodel as active
        }
    }
}
