using Caliburn.Micro;
using Network;
using SCPackets.UpdateRoomData;
using SharpDj.PubSubModels;
using System.Collections.Generic;

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
            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.AlreadyExist, new MessageQueue("Update room","Room with given name already exists")},
                {Result.Error, new MessageQueue("Update room","Unexpected error")},
                {Result.NameError, new MessageQueue("Update room","Given room name is not valid")},
                {Result.ImageError, new MessageQueue("Update room","Given Image is not valid")},
                {Result.LocalMessageError, new MessageQueue("Update room","One of local message is not valid")},
                {Result.PublicMessageError, new MessageQueue("Update room","One of public message is not valid")},
                {Result.Success, new MessageQueue("Update room","Room has been updated!")},
            };

            if (response.Result == Result.Success)
                _eventAggregator.PublishOnUIThread(
                    new ManageRoomsUpdatedPublish(response.Room));

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
