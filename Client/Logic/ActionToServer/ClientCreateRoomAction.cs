using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.CreateRoom;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientCreateRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientCreateRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(CreateRoomResponse response, Connection connection)
        {
            Console.WriteLine(response.Result);
        }
    }
}
