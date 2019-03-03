using System.Collections.Generic;
using System.Threading;
using Communication.Client;
using Newtonsoft.Json;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client.Commands
{
    public class RemoveUserFromRoomCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["RemoveUserFromRoom"];

        public void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var uiContext = SynchronizationContext.Current;

            var json = JsonConvert.DeserializeObject<UserClient>(parameters[0]);
            var roomId = parameters[1];

            uiContext.Send(x => sdjMainViewModel.SdjRoomViewModel.UserList.RemoveAt(json), null);
        }
    }
}