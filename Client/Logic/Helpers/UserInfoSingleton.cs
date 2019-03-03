using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Client.User;
using Communication.Shared;
using Communication.Shared.Data;

namespace SharpDj.Logic.Helpers
{
    public sealed class UserInfoSingleton
    {
        private static readonly Lazy<UserInfoSingleton> lazy =
            new Lazy<UserInfoSingleton>(() => new UserInfoSingleton());

        public static UserInfoSingleton Instance => lazy.Value;

        private UserInfoSingleton()
        {

        }

       public UserClient UserClient { get; set; } = new UserClient(0, "Crisey", Rank.Admin);
    }
}