using System;
using System.Collections.Generic;
using System.Linq;
using Hik.Communication.Scs.Client;
using SharpDj.Logic.Client.Commands;
using SharpDj.Logic.Helpers;
using SharpDj.ViewModel;

namespace SharpDj.Logic.Client
{
    public class ClientReceiver
    {
        private readonly List<ICommand> _commands;
        private readonly SdjMainViewModel _sdjMainViewModel;

        public ClientReceiver(SdjMainViewModel sdjMainViewModel)
        {
            this._sdjMainViewModel = sdjMainViewModel;
            _commands = new List<ICommand>()
            {
                new UpdateDjCommand(),
                new SendMessageChatCommand(),
                new AddUserToRoomCommand(),
                new RemoveUserFromRoomCommand(),
                new ChangeTrackCommand(),
                new JoinQueueCommand(),
            };
        }

        public void ParseMessage(IScsClient client, string message)
        {
            var command = Communication.Shared.Commands.Instance.GetMessageCommand(message);
            var commandClass = _commands.FirstOrDefault(x => x.CommandText.Equals(command));
            try
            {
                commandClass?.Run(_sdjMainViewModel, Communication.Shared.Commands.Instance.GetMessageParameters(message));
            }
            catch (Exception e)
            {
                new ExceptionLogger(e);
            }
        }
    }
}