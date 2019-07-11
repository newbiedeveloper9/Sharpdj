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
        public UserClientModel Client { get; }

        public LoginPublish(UserClientModel client)
        {
            Client = client;
        }

        public LoginPublish()
        {
            
        }
    }

    public interface ILoginPublish
    {
        UserClientModel Client { get; }
    }
}
