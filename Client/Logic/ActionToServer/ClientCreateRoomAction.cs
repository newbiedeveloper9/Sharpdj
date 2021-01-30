using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.Packets.CreateRoom;
using SharpDj.Enums;
using SharpDj.PubSubModels;

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
            var dictionaryMessages = new Dictionary<CreateRoomResult, MessageQueue>()
            {
                {CreateRoomResult.AlreadyExist, new MessageQueue("Create room","Room with given name already exists")},
                {CreateRoomResult.Error, new MessageQueue("Create room","Unexpected error")},
                {CreateRoomResult.NameError, new MessageQueue("Create room","Given room name is not valid")},
                {CreateRoomResult.ImageError, new MessageQueue("Create room","Given Image is not valid")},
                {CreateRoomResult.LocalMessageError, new MessageQueue("Create room","One of local message is not valid")},
                {CreateRoomResult.PublicMessageError, new MessageQueue("Create room","One of public message is not valid")},
                {CreateRoomResult.Success, new MessageQueue("Create room","Room has been created!")},
            };

            if (response.Result == CreateRoomResult.Success)
            {
                _eventAggregator.PublishOnUIThread(new CreatedRoomPublish(response.RoomDetails));
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
