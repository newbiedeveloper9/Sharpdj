using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messengers;

namespace SharpDj.Models.Client
{
    public static class ClientInfo
    {
        public static RequestReplyMessenger<IScsClient> ReplyMessenger { get; set; }
        public static IScsClient Client { get; set; }
        
    }
}