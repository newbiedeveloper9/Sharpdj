using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.AuthKeyLogin;
using SCPackets.Buffers;
using SCPackets.ConnectToRoom;
using SCPackets.CreateRoom;
using SCPackets.Disconnect;
using SCPackets.LoginPacket;
using SCPackets.NotLoggedIn;
using SCPackets.Ping;
using SCPackets.PullPostsInRoom;
using SCPackets.RegisterPacket;
using SCPackets.RoomChatNewMessageClient;
using SCPackets.SendRoomChatMessage;
using SCPackets.UpdateRoomData;
using SharpDj.Logic.ActionToServer;

namespace SharpDj.Logic
{
    class ClientPacketsToHandleList
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly List<IHandlerModel> _handlers = new List<IHandlerModel>();

        public ClientPacketsToHandleList(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            var login = new ClientLoginAction(_eventAggregator);
            var register = new ClientRegisterAction(_eventAggregator);
            var createRoom = new ClientCreateRoomAction(_eventAggregator);
            var updateRoom = new ClientUpdateRoomAction(_eventAggregator);
            var disconnect = new ClientDisconnectAction(_eventAggregator);
            var sendRoomChatMessage = new ClientSendRoomChatMessageAction(_eventAggregator);
            var connectToRoom = new ClientConnectToRoomAction(_eventAggregator);
            var notLoggedIn = new ClientNotLoggedInAction(_eventAggregator);
            var ping = new ClientPingAction(_eventAggregator);
            var roomUserListBuffer = new ClientRoomUserListBufferAction(_eventAggregator);
            var squareRoomBuffer = new ClientSquareRoomBufferAction(_eventAggregator);
            var roomChatNewMessage = new ClientRoomChatNewMessageAction(_eventAggregator);
            var authKeyLogin = new ClientAuthKeyLoginAction(_eventAggregator);
            var pullPostsInRoom = new ClientPullPostsInRoomAction(_eventAggregator);

            _handlers.Add(new HandlerModel<LoginResponse>(login.Action));
            _handlers.Add(new HandlerModel<RegisterResponse>(register.Action));
            _handlers.Add(new HandlerModel<CreateRoomResponse>(createRoom.Action));
            _handlers.Add(new HandlerModel<UpdateRoomDataResponse>(updateRoom.Action));
            _handlers.Add(new HandlerModel<DisconnectResponse>(disconnect.Action));
            _handlers.Add(new HandlerModel<SendRoomChatMessageResponse>(sendRoomChatMessage.Action));
            _handlers.Add(new HandlerModel<ConnectToRoomResponse>(connectToRoom.Action));
            _handlers.Add(new HandlerModel<NotLoggedInRequest>(notLoggedIn.Action));
            _handlers.Add(new HandlerModel<PingRequest>(ping.Action));
            _handlers.Add(new HandlerModel<RoomUserListBufferRequest>(roomUserListBuffer.Action));
            _handlers.Add(new HandlerModel<SquareRoomBufferRequest>(squareRoomBuffer.Action));
            _handlers.Add(new HandlerModel<RoomChatNewMessageRequest>(roomChatNewMessage.Action));
            _handlers.Add(new HandlerModel<AuthKeyLoginResponse>(authKeyLogin.Action));
            _handlers.Add(new HandlerModel<PullPostsInRoomResponse>(pullPostsInRoom.Action));
        }

        public void RegisterPackets(Connection conn, IClient client)
        {
            _handlers.ForEach(x => x.RegisterPacket(conn, client));
        }
    }
}
