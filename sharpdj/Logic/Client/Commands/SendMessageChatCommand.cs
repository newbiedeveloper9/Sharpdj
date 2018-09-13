using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Communication.Server;
using SharpDj.ViewModel;
using SharpDj.ViewModel.Model;

namespace SharpDj.Logic.Client.Commands
{
    public class SendMessageChatCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["SendMessage"];

        public void Run(SdjMainViewModel sdjMainViewModel,
            List<string> parameters)
        {
            
            var text = parameters[0];
            var roomId = parameters[1];
            var userId = parameters[2];
            var time = DateTime.Now.ToShortTimeString();


            var mess = new RoomMessageModel(sdjMainViewModel)
            {
                Username = "",
                Time = time,
                Message = text
            };
            
            var username = sdjMainViewModel.SdjRoomViewModel.UserList
                .First(x => x.Id == Convert.ToInt64(userId))
                ?.Username;
            

            var tmp = new ObservableCollection<RoomMessageModel>(
                sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection) {mess};
            sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection = tmp;
        }
    }
}