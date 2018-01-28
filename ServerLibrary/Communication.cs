using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace ServerLibrary
{
    public class Communication
    {
        public static class MessagesContains
        {
            public static string DisconnectCtn { get; set; } = "disc";
            public static string LoginCtn { get; set; } = "login ";
        }

        public static class MessagesPattern
        {
            public static string LoginRgx { get; private set; } = "login (.*) (.*)";
        }


        public void SendMessageToAllClients(IScsServer server, string message)
        {
            foreach (var clients in server.Clients.GetAllItems())
            {
                clients.SendMessage(new ScsTextMessage(message));
            }
        }

        public void ParseMessage(IScsServer server, IScsServerClient client, string message)
        {
            if (message.StartsWith(MessagesContains.DisconnectCtn))
                OnDisconnect(EventArgs.Empty);
            else if (message.StartsWith(MessagesContains.LoginCtn))
            {
                Regex rgx = new Regex(MessagesPattern.LoginRgx);
                Match match = rgx.Match(message);
                if (match.Success)
                {
                    var login = match.Groups[1].Value;
                    var password = match.Groups[2].Value;
                    OnLogin(new OnLoginEventArgs(login, password, client.RemoteEndPoint.ToString()));
                }
                else
                {
                    Console.WriteLine("Fail in login");
                }
            }
        }

        #region Events
        public event EventHandler Disconnect;
        public event EventHandler<OnLoginEventArgs> Login;


        internal virtual void OnDisconnect(EventArgs e)
        {
            EventHandler eh = Disconnect;
            eh?.Invoke(this, e);
        }

        internal virtual void OnLogin(OnLoginEventArgs e)
        {
            EventHandler<OnLoginEventArgs> eh = Login;
            eh?.Invoke(this, e);
        }


        public class OnLoginEventArgs : System.EventArgs
        {
            public OnLoginEventArgs(string login, string password, string ip)
            {
                this.Login = login;
                this.Password = password;
                this.Ip = ip;
            }

            public string Login { get; private set; }
            public string Password { get; private set; }
            public string Ip { get; private set; }
        }
        #endregion
    }
}
