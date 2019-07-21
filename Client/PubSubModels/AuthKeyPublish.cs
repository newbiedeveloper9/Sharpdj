using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.PubSubModels
{
    public class AuthKeyPublish : IAuthKeyPublish
    {
        public AuthKeyPublish(string authKey)
        {
            AuthKey = authKey;
        }

        public string AuthKey { get; set; }
    }

    public interface IAuthKeyPublish
    {
        string AuthKey { get; set; }
    }
}
