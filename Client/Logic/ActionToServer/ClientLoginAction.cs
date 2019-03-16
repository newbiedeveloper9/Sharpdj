using Caliburn.Micro;
using Network;
using SCPackets.LoginPacket;
using SCPackets.LoginPacket.Container;
using SharpDj.PubSubModels;
using System.Collections.Generic;

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
                {Result.AlreadyLogged, new MessageQueue("Login", "Error, You were already logged in.") },
            };

            if (response.Result == Result.Success)
            {
                if (response.RoomOutsideModelList?.Count > 0)
                    _eventAggregator.PublishOnUIThread(new RefreshOutsideRoomsPublish(response.RoomOutsideModelList));
                if (response.UserRoomList?.Count > 0)
                    _eventAggregator.PublishOnUIThread(new ManageRoomsPublish(response.UserRoomList));
                _eventAggregator.PublishOnUIThread(new LoginPublish(response.User));
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
