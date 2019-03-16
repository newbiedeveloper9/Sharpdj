using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.UpdateRoomData;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientUpdateRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientUpdateRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(UpdateRoomDataResponse response, Connection connection)
        {
            Console.WriteLine(response.Result);
        }
    }
}
