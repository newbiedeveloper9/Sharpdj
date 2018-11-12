using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Communication.Server;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using SharpDj.Logic.Client.Commands;
using SharpDj.Logic.Helpers;
using SharpDj.ViewModel;

namespace Communication.Client.Logic
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
            };
        }

        public void ParseMessage(IScsClient client, string message)
        {
            var command = Commands.Instance.GetMessageCommand(message);
            var commandClass = _commands.FirstOrDefault(x => x.CommandText.Equals(command));
            try
            {
                commandClass?.Run(_sdjMainViewModel, Commands.Instance.GetMessageParameters(message));
            }
            catch (Exception e)
            {
                new ExceptionLogger(e);
            }
        }
    }
}