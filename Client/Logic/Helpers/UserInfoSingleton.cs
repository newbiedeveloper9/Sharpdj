using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCPackets;

namespace SharpDj.Logic.Helpers
{
    public sealed class UserInfoSingleton
    {
        private static readonly Lazy<UserInfoSingleton> lazy =
            new Lazy<UserInfoSingleton>(() => new UserInfoSingleton());

        public static UserInfoSingleton Instance => lazy.Value;

        private UserInfoSingleton()
        {
            UserClient = new UserClient();
        }

       public UserClient UserClient { get; set; }
    }
}