using Caliburn.Micro;
using SharpDj.PubSubModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCPackets.Packets.Disconnect;
using SCPackets.Packets.Login;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientLoginAction : IAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientLoginAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public async Task Action(LoginResponse response)
        {
            var data = response.Data;
            var dictionaryMessages = new Dictionary<LoginResult, MessageQueue>()
            {
                {LoginResult.Error, new MessageQueue("Login", "System error!") },
                {LoginResult.CredentialsError, new MessageQueue("Login","We have encountered a problem with your credentials. Please, try again.") },
                {LoginResult.AlreadyLogged, new MessageQueue("Login", "Error, this user is already logged in.") },
                {LoginResult.AlreadyLoggedError, new MessageQueue("Login", "Error, you are already logged in. Disconnecting.") }
            };

            if (response.Result == LoginResult.Success)
            {
                if (data.RoomOutsideModelList?.Count > 0)
                    await _eventAggregator.PublishOnUIThreadAsync(new RefreshOutsideRoomsPublish(data.RoomOutsideModelList));

                await _eventAggregator.PublishOnUIThreadAsync(new ManageRoomsPublish(data.UserRoomList));
                await _eventAggregator.PublishOnUIThreadAsync(new LoginPublish(data.User));

                var authKey = response.AuthenticationKey;
                if (!string.IsNullOrWhiteSpace(authKey))
                    await _eventAggregator.PublishOnUIThreadAsync(new AuthKeyPublish(authKey));

                return;
            }

            if (response.Result == LoginResult.AlreadyLoggedError)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new SendPacket(new DisconnectRequest()));
            }

            await _eventAggregator.PublishOnUIThreadAsync(dictionaryMessages[response.Result]);
        }
    }
}
