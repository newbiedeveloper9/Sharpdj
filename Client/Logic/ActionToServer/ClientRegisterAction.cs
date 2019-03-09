using Caliburn.Micro;
using Network;
using SCPackets.RegisterPacket;
using SharpDj.PubSubModels;
using System.Collections.Generic;
using SharpDj.Enums;

namespace SharpDj.Logic.ActionToServer
{
    class ClientRegisterAction
    {
        private readonly IEventAggregator _eventAggregator;

        public ClientRegisterAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Action(RegisterResponse response, Connection connection)
        {
            var dictionaryMessages = new Dictionary<Result, MessageQueue>()
            {
                {Result.AlreadyExist, new MessageQueue("Register", "Account with given login/email already exist.") },
                {Result.Error, new MessageQueue("Register", "Error occurred while attempting to create new account.") },
                {Result.PasswordError, new MessageQueue("Register", "Too weak password.") },
                {Result.Success, new MessageQueue("Register", "Account has been created successfully") },
                {Result.UsernameError, new MessageQueue("Register", "Your Username must be between 2 and 48 characters") },
                {Result.LoginError, new MessageQueue("Register", "Your Login must be between 2 and 48 characters") },
                {Result.EmailError, new MessageQueue("Register", "E-mail isn't valid") }
            };

            if (response.Result == Result.Success)
            {
                _eventAggregator.PublishOnUIThread(BeforeLoginEnum.Login);
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
