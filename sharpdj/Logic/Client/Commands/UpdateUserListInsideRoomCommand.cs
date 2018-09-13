using System;
using System.Collections.Generic;
using Communication.Client;
using Newtonsoft.Json;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client.Commands
{
    public class UpdateUserListInsideRoomCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["UpdateUserListInsideRoom"];

        public void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var json = parameters[0];
            var roomId = parameters[1];

            sdjMainViewModel.SdjRoomViewModel.UserList =
                JsonConvert.DeserializeObject<List<UserClient>>(json);
        }
    }
}