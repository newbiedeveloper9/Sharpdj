using System;
using Caliburn.Micro;
using SharpDj.Models;

namespace SharpDj.ViewModels.SubViews.MainViewComponents.LeftMenuComponents
{
    public class ObservedRoomViewModel : Screen
    {
        public BindableCollection<RoomModel> ObservedRoomCollection { get; private set; }

        public ObservedRoomViewModel()
        {
           
        }

        protected override void OnViewLoaded(object view)
        {
            Console.WriteLine("Observed loaded");

            var dicPic = @"..\..\..\..\Images\3.jpg";

            ObservedRoomCollection = new BindableCollection<RoomModel>()
            {
                new RoomModel(){AmountOfAdministration = 3, AmountOfPeople = 49, Id = 1, ImageSource = dicPic, Name = "Testowa nazwa pokoju"},
                new RoomModel(){AmountOfAdministration = 4, AmountOfPeople = 23, Id = 2, ImageSource = dicPic, Name = "Pokój"},
            };
            base.OnViewLoaded(view);
        }
    }
}
