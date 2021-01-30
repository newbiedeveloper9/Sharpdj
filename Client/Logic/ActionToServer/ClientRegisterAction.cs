using Caliburn.Micro;
using Network;
using SharpDj.PubSubModels;
using System.Collections.Generic;
using SCPackets.Packets.Register;
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
            var dictionaryMessages = new Dictionary<RegisterResult, MessageQueue>()
            {
                {RegisterResult.AlreadyExist, new MessageQueue("Register", "Account with given login/email already exist.") },
                {RegisterResult.Error, new MessageQueue("Register", "Error occurred while attempting to create new account.") },
                {RegisterResult.PasswordError, new MessageQueue("Register", "Too weak password.") },
                {RegisterResult.Success, new MessageQueue("Register", "Account has been created successfully") },
                {RegisterResult.UsernameError, new MessageQueue("Register", "Your Username must be between 2 and 48 characters") },
                {RegisterResult.LoginError, new MessageQueue("Register", "Your Login must be between 2 and 48 characters") },
                {RegisterResult.EmailError, new MessageQueue("Register", "E-mail isn't valid") }
            };

            if (response.Result == RegisterResult.Success)
            {
                _eventAggregator.PublishOnUIThread(BeforeLoginEnum.Login);
            }

            _eventAggregator.PublishOnUIThread(dictionaryMessages[response.Result]);
        }
    }
}
