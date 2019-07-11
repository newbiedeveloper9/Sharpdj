﻿using System;
using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.ConnectToRoom;
using SCPackets.CreateRoom;
using SCPackets.Disconnect;
using SCPackets.LoginPacket;
using SCPackets.NewRoomCreated;
using SCPackets.NotLoggedIn;
using SCPackets.Ping;
using SCPackets.RegisterPacket;
using SCPackets.RoomChatNewMessageClient;
using SCPackets.RoomOutsideUpdate;
using SCPackets.SendRoomChatMessage;
using SCPackets.UpdateRoomData;
using SharpDj.Logic.ActionToServer;

namespace SharpDj.Logic
{
    class ClientPacketsToHandleList
    {
        private readonly IEventAggregator _eventAggregator;

        public HandlerModel<CreateRoomResponse> CreateRoom { get; set; }
        public HandlerModel<LoginResponse> Login { get; set; }
        public HandlerModel<RegisterResponse> Register { get; set; }
        public HandlerModel<NotLoggedInRequest> NotLoggedIn { get; set; }
        public HandlerModel<NewRoomCreatedRequest> NewRoomCreated { get; set; }
        public HandlerModel<UpdateRoomDataResponse> RoomUpdate { get; set; }
        public HandlerModel<RoomOutsideUpdateRequest> RoomOutsideUpdate { get; set; }
        public HandlerModel<DisconnectResponse> Disconnect { get; set; }
        public HandlerModel<SendRoomChatMessageResponse> SendRoomChatMessage { get; set; }
        public HandlerModel<RoomChatNewMessageRequest> RoomChatNewMessage { get; set; }
        public HandlerModel<ConnectToRoomResponse> ConnectToRoom { get; set; }
        public HandlerModel<PingRequest> Ping { get; set; }

        public ClientPacketsToHandleList(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Login = new HandlerModel<LoginResponse>
            { Action = new ClientLoginAction(_eventAggregator).Action };
            Register = new HandlerModel<RegisterResponse>
            { Action = new ClientRegisterAction(_eventAggregator).Action };
            CreateRoom = new HandlerModel<CreateRoomResponse>
            { Action = new ClientCreateRoomAction(_eventAggregator).Action };
            NotLoggedIn = new HandlerModel<NotLoggedInRequest>
            { Action = new ClientNotLoggedInAction(_eventAggregator).Action };
            NewRoomCreated = new HandlerModel<NewRoomCreatedRequest>
            { Action = new ClientNewRoomCreatedAction(_eventAggregator).Action };
            RoomUpdate = new HandlerModel<UpdateRoomDataResponse>
            { Action = new ClientUpdateRoomAction(_eventAggregator).Action };
            RoomOutsideUpdate = new HandlerModel<RoomOutsideUpdateRequest>
            { Action = new ClientRoomOutsideUpdateAction(_eventAggregator).Action };
            Disconnect = new HandlerModel<DisconnectResponse>
            { Action = new ClientDisconnectAction(_eventAggregator).Action };
            SendRoomChatMessage = new HandlerModel<SendRoomChatMessageResponse>
            { Action = new ClientSendRoomChatMessageAction(_eventAggregator).Action };
            ConnectToRoom = new HandlerModel<ConnectToRoomResponse>
            { Action = new ClientConnectToRoomAction(_eventAggregator).Action };
            RoomChatNewMessage = new HandlerModel<RoomChatNewMessageRequest>()
            { Action = new ClientRoomChatNewMessageAction(_eventAggregator).Action };
            Ping = new HandlerModel<PingRequest>
            { Action = new ClientPingAction(_eventAggregator).Action };


        }

        public void RegisterPackets(Connection conn, IClient client)
        {
            Login.RegisterPacket(conn, client);
            Register.RegisterPacket(conn, client);
            CreateRoom.RegisterPacket(conn, client);
            NotLoggedIn.RegisterPacket(conn, client);
            NewRoomCreated.RegisterPacket(conn, client);
            RoomUpdate.RegisterPacket(conn, client);
            RoomOutsideUpdate.RegisterPacket(conn, client);
            Disconnect.RegisterPacket(conn, client);
            SendRoomChatMessage.RegisterPacket(conn, client);
            ConnectToRoom.RegisterPacket(conn, client);
           RoomChatNewMessage.RegisterPacket(conn, client);
            Ping.RegisterPacket(conn, client);
        }
    }
}
