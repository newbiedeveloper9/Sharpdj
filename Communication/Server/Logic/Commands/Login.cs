using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Communication.Server.Logic.Commands;
using Communication.Shared;
using Hik.Communication.Scs.Server;

namespace Communication.Server.Logic
{
    public class LoginCommand : ICommand
    {
        public string CommandText { get; } = Shared.Commands.Instance.CommandsDictionary["Login"];
        

        public void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var login = parameters[1];
            var password = parameters[2];

            OnLogin(new LoginEventArgs(login, password, client, messageId));
        }
        
        public event EventHandler<LoginEventArgs> Login;
        public void OnLogin(LoginEventArgs e)
        {
            EventHandler<LoginEventArgs> eh = Login;
            eh?.Invoke(this, e);
        }
        
        public class LoginEventArgs : System.EventArgs
        {
            public LoginEventArgs(string login, string password, IScsServerClient client, string messageId)
            {
                this.Login = login;
                this.Password = password;
                this.Client = client;
                this.MessageId = messageId;
            }

            public string Login { get; private set; }
            public string Password { get; private set; }
            public IScsServerClient Client { get; private set; }
            public string MessageId { get; private set; }
        }
    }
}