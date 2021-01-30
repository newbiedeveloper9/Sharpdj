using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets;
using SCPackets.Models;

namespace SharpDj.PubSubModels
{
    public class LoginPublish : ILoginPublish
    {
        public UserClient Client { get; }

        public LoginPublish(UserClient client)
        {
            Client = client;
        }

        public LoginPublish()
        {
            
        }
    }

    public interface ILoginPublish
    {
        UserClient Client { get; }
    }
}
