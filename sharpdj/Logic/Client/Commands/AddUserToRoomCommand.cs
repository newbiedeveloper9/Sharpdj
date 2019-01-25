using System;
using System.Collections.Generic;
using Communication.Client;
using Newtonsoft.Json;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client.Commands
{
    public class AddUserToRoomCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["AddUserToRoom"];

        public void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var json = JsonConvert.DeserializeObject<UserClient>(parameters[0]);
            var roomId = parameters[1];

            sdjMainViewModel.SdjRoomViewModel.UserList.Add(json);
        }
    }
} 