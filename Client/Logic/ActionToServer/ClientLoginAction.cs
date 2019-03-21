using Caliburn.Micro;
using Network;
using SCPackets.Disconnect;
using SCPackets.LoginPacket;
using SharpDj.PubSubModels;
using System.Collections.Generic;
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

            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.Success, new MessageQueue("Login", "You have been successfully logged in!") },
                {Result.Error, new MessageQueue("Login", "System error!") },
                {Result.CredentialsError, new MessageQueue("Login","We have encountered a problem with your credentials. Please, try again.") },
                {Result.AlreadyLogged, new MessageQueue("Login", "Error, this user is already logged in.") },
                {Result.AlreadyLoggedError, new MessageQueue("Login", "Error, you are already logged in. Disconnecting.") }
            };

            if (response.Result == Result.Success)
            {
                if (response.RoomOutsideModelList?.Count > 0)
                    _eventAggregator.PublishOnUIThread(new RefreshOutsideRoomsPublish(response.RoomOutsideModelList));
                _eventAggregator.PublishOnUIThread(new ManageRoomsPublish(response.UserRoomList));
                _eventAggregator.PublishOnUIThread(new LoginPublish(response.User));
            }

            if (response.Result == Result.AlreadyLoggedError)
            {
                _eventAggregator.PublishOnUIThread(new SendPacket(new DisconnectRequest()));
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
