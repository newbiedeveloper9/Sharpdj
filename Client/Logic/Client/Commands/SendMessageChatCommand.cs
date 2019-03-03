using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
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
            var uiContext = SynchronizationContext.Current;

            var text = parameters[0];
            var roomId = parameters[1];
            var userId = parameters[2];


            var mess = new RoomMessageModel(sdjMainViewModel)
            {
                Time = DateTime.Now.ToShortTimeString(),
                Message = text,
                Username = sdjMainViewModel.SdjRoomViewModel.UserList
                    .First(x => x.Id == Convert.ToInt64(userId))
                    ?.Username
            };
            uiContext.Send(x => sdjMainViewModel.SdjRoomViewModel.RoomMessageCollection.Add(mess), null);
        }
    }
}