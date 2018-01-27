using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Shared;
using Hik.Communication.Scs.Server;

namespace Communication.Server
{
    public class ServerReceiver
    {
        public void ParseMessage(IScsServerClient client, string message)
        {
            var msg = message.Remove(message.IndexOf(":"));

            Console.WriteLine(msg);

            if (message == CommandsPacket.LoginAsGuest.ToString())
            {
               OnLoginAsGuest(new LoginAsGuestEventArgs(client));
            }
        }


        public event EventHandler<LoginAsGuestEventArgs> LoginAsGuest;


        internal virtual void OnLoginAsGuest(LoginAsGuestEventArgs e)
        {
            var handler = LoginAsGuest;
            handler?.Invoke(this, e);
        }

        public class LoginAsGuestEventArgs : System.EventArgs
        {
            public LoginAsGuestEventArgs(IScsServerClient client)
            {
                this.Client = client;
            }

            public IScsServerClient Client { get; private set; }
        }
    }
}
