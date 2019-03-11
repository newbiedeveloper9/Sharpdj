using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Network;
using SCPackets.CreateRoom;
using SCPackets.CreateRoom.Container;
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
            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.AlreadyExist, new MessageQueue("Create room","Room with given name already exists")},
                {Result.Error, new MessageQueue("Create room","Unexpected error")},
                {Result.NameError, new MessageQueue("Create room","Given room name is not valid")},
                {Result.ImageError, new MessageQueue("Create room","Given Image is not valid")},
                {Result.LocalMessageError, new MessageQueue("Create room","One of local message is not valid")},
                {Result.PublicMessageError, new MessageQueue("Create room","One of public message is not valid")},
                {Result.Success, new MessageQueue("Create room","Room has been created!")},
            };

            if (response.Result == Result.Success)
            {
                _eventAggregator.PublishOnUIThread(NavigateMainView.Room);
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
