using Caliburn.Micro;
using Network;
using SharpDj.PubSubModels;
using System.Collections.Generic;
using SCPackets.Packets.UpdateRoom;

namespace SharpDj.Logic.ActionToServer
{
    public class ClientUpdateRoomAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientUpdateRoomAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(UpdateRoomResponse response, Connection connection)
        {
            var dictionaryMessages = new Dictionary<UpdateRoomResult, MessageQueue>()
            {
                {UpdateRoomResult.AlreadyExist, new MessageQueue("Update room","Room with given name already exists")},
                {UpdateRoomResult.Error, new MessageQueue("Update room","Unexpected error")},
                {UpdateRoomResult.NameError, new MessageQueue("Update room","Given room name is not valid")},
                {UpdateRoomResult.ImageError, new MessageQueue("Update room","Given Image is not valid")},
                {UpdateRoomResult.LocalMessageError, new MessageQueue("Update room","One of local message is not valid")},
                {UpdateRoomResult.PublicMessageError, new MessageQueue("Update room","One of public message is not valid")},
                {UpdateRoomResult.Success, new MessageQueue("Update room","Room has been updated!")},
            };

            if (response.Result == UpdateRoomResult.Success)
                _eventAggregator.PublishOnUIThread(
                    new ManageRoomsUpdatedPublish(response.RoomDetails));

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
