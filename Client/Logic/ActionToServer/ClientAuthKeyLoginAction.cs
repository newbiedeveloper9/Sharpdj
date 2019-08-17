using System;
using System.Collections.Generic;
using System.Threading;
using Caliburn.Micro;
using Network;
using SCPackets.AuthKeyLogin;
using SCPackets.Disconnect;
using SharpDj.PubSubModels;
using Result = SCPackets.AuthKeyLogin.Result;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientAuthKeyLoginAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientAuthKeyLoginAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(AuthKeyLoginResponse response, Connection connection)
        {
            var data = response.Data;
            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.Error, new MessageQueue("Login", "We have encountered a problem with your credentials. Please, try again.") },
                {Result.Expired, new MessageQueue("Login","It seems that your key expired. You have to login again.") },
                {Result.AlreadyLogged, new MessageQueue("Login", "Error, this user is already logged in.") },
            };

            if (response.Result == Result.Success)
            {
#if !DEBUG
                Thread.Sleep(450);//todo wait for call instead of sleep
#endif
                if (data.RoomOutsideModelList?.Count > 0)
                    _eventAggregator.BeginPublishOnUIThread(new RefreshOutsideRoomsPublish(data.RoomOutsideModelList));

                _eventAggregator.BeginPublishOnUIThread(new ManageRoomsPublish(data.UserRoomList));
                _eventAggregator.BeginPublishOnUIThread(new LoginPublish(data.User));

                return;
            }
            if (response.Result == Result.AlreadyLogged)
            {
                _eventAggregator.BeginPublishOnUIThread(new SendPacket(new DisconnectRequest()));
            }

            _eventAggregator.BeginPublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}