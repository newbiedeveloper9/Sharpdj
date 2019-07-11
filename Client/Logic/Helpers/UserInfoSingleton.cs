using System;
using SCPackets.Models;
using SharpDj.Models;

namespace SharpDj.Logic.Helpers
{
    public sealed class UserInfoSingleton
    {
        private static readonly Lazy<UserInfoSingleton> lazy =
            new Lazy<UserInfoSingleton>(() => new UserInfoSingleton());

        public static UserInfoSingleton Instance => lazy.Value;

        private UserInfoSingleton()
        {
            UserClient = new UserClientModel();
        }

       public UserClientModel UserClient { get; set; }
       public RoomModel ActiveRoom { get; set; }
    }
}