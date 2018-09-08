using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var text = parameters[1];
            var roomId = parameters[2];
            var userId = parameters[3];
            var time = DateTime.Now.ToShortTimeString();
            
            
            var mess = new RoomMessageModel(sdjMainViewModel)
            {
                Message = text,
                Time = time,
                Username = userId
            };
         //   sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection = new ObservableCollection<RoomMessageModel>();
            var tmp = new ObservableCollection<RoomMessageModel>(sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection);
            tmp.Add(mess);
            sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection = tmp;
            
            
            //   sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection.Add(roomMessage);
        }
    }
}