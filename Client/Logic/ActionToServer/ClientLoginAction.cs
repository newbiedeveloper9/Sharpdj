using Caliburn.Micro;
using Network;
using SCPackets.Disconnect;
using SCPackets.LoginPacket;
using SharpDj.PubSubModels;
using System.Collections.Generic;
using System.Threading;
using SCPackets.Models;
using Result = SCPackets.LoginPacket.Container.Result;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientLoginAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientLoginAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(LoginResponse response, Connection connection)
        {
            var data = response.Data;
            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.Error, new MessageQueue("Login", "System error!") },
                {Result.CredentialsError, new MessageQueue("Login","We have encountered a problem with your credentials. Please, try again.") },
                {Result.AlreadyLogged, new MessageQueue("Login", "Error, this user is already logged in.") },
                {Result.AlreadyLoggedError, new MessageQueue("Login", "Error, you are already logged in. Disconnecting.") }
            };

            if (response.Result == Result.Success)
            {
                if (data.RoomOutsideModelList?.Count > 0)
                    _eventAggregator.PublishOnUIThreadAsync(new RefreshOutsideRoomsPublish(data.RoomOutsideModelList));

                _eventAggregator.PublishOnUIThreadAsync(new ManageRoomsPublish(data.UserRoomList));
                _eventAggregator.PublishOnUIThreadAsync(new LoginPublish(data.User));

                var authKey = response.AuthenticationKey;
                if (!string.IsNullOrWhiteSpace(authKey))
                    _eventAggregator.PublishOnUIThreadAsync(new AuthKeyPublish(authKey));

                return;
            }

            if (response.Result == Result.AlreadyLoggedError)
            {
                _eventAggregator.PublishOnUIThreadAsync(new SendPacket(new DisconnectRequest()));
            }

            _eventAggregator.PublishOnUIThreadAsync(dictionaryMessages[response.Result]);
        }
    }
}
