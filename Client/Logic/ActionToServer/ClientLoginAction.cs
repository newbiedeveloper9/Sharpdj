using Caliburn.Micro;
using Network;
using SCPackets.LoginPacket;
using System;
using System.Collections.Generic;
using SCPackets;
using SharpDj.Logic.Helpers;
using SharpDj.PubSubModels;

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
                {Result.Error, new MessageQueue("Login", "Authentication error!") }
            };

            if (response.Result == Result.Success)
            {
                _eventAggregator.PublishOnUIThread(new LoginPublishInfo());
                UserInfoSingleton.Instance.UserClient = new UserClient(0, "Crisey", Rank.Admin);
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
