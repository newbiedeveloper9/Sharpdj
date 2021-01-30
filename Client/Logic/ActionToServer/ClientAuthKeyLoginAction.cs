using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.AuthKeyLogin;
using SCPackets.Packets.Disconnect;
using SharpDj.PubSubModels;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientAuthKeyLoginAction : ActionAbstract<AuthKeyLoginResponse>
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientAuthKeyLoginAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public override async Task Action(AuthKeyLoginResponse response, Connection connection)
        {
            var data = response.Data;
            var dictionaryMessages = new Dictionary<AuthKeyLoginResult, MessageQueue>()
            {
                {AuthKeyLoginResult.Error, new MessageQueue("Login", "We have encountered a problem with your credentials. Please, try again.") },
                {AuthKeyLoginResult.Expired, new MessageQueue("Login","It seems that your key expired. You have to login again.") },
                {AuthKeyLoginResult.AlreadyLogged, new MessageQueue("Login", "Error, this user is already logged in.") },
            };

            if (response.Result == AuthKeyLoginResult.Success)
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
            if (response.Result == AuthKeyLoginResult.AlreadyLogged)
            {
                _eventAggregator.BeginPublishOnUIThread(new SendPacket(new DisconnectRequest()));
            }

            _eventAggregator.BeginPublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}