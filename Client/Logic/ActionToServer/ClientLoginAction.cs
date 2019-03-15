using System;
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
                {Result.Error, new MessageQueue("Login", "Authentication error!") },
                {Result.AlreadyLogged, new MessageQueue("Login", "Error, You are currently logged in") }
            };

            if (response.Result == Result.Success)
            {
                if (response.RoomOutsideModelList?.Count > 0)
                    _eventAggregator.PublishOnUIThread(new RefreshOutsideRoomsPublish(response.RoomOutsideModelList));
                _eventAggregator.PublishOnUIThread(new LoginPublish(response.User));
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
