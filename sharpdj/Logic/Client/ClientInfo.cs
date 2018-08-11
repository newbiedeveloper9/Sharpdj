using System;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.Messengers;

namespace SharpDj.Logic.Client
{
    public sealed class ClientInfo
    {
        private static readonly Lazy<ClientInfo> lazy =
             new Lazy<ClientInfo>(() => new ClientInfo());

        public static ClientInfo Instance => lazy.Value;

        private ClientInfo()
        {

        }

        public RequestReplyMessenger<IScsClient> ReplyMessenger { get; set; }
        public IScsClient Client { get; set; }
    }
}