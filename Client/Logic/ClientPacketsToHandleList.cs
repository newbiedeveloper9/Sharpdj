using Caliburn.Micro;
using Network;
using SCPackets;
using SCPackets.CreateRoom;
using SCPackets.LoginPacket;
using SCPackets.RegisterPacket;
using SharpDj.Logic.ActionToServer;

namespace SharpDj.Logic
{
    class ClientPacketsToHandleList
    {
        private readonly IEventAggregator _eventAggregator;

        public HandlerModel<CreateRoomResponse> CreateRoom { get; set; }
        public HandlerModel<LoginResponse> Login { get; set; }
        public HandlerModel<RegisterResponse> Register { get; set; }

        public ClientPacketsToHandleList(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Login = new HandlerModel<LoginResponse> { Action = new ClientLoginAction(_eventAggregator).Action };
            Register = new HandlerModel<RegisterResponse> { Action = new ClientRegisterAction(_eventAggregator).Action };
            CreateRoom = new HandlerModel<CreateRoomResponse> { Action = new ClientCreateRoomAction(_eventAggregator).Action };
        }

        public void RegisterPackets(Connection conn, IClient client)
        {
            Login.RegisterPacket(conn, client);
            Register.RegisterPacket(conn, client);
            CreateRoom.RegisterPacket(conn, client);
        }
    }
}
