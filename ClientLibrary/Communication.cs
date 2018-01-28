using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messages;


namespace ClientLibrary
{
    public static class Communication
    {
        public static void Login(IScsClient client, string username, string password)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format("login {0} {1}", username, password)));
        }

        public static void Disconnect(IScsClient client)
        {
            client.SendMessage(new ScsTextMessage(
                String.Format("disc")));
        }
    }
}
