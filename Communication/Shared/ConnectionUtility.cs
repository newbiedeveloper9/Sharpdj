using System.Text;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;

namespace Communication.Shared
{
    public class ConnectionUtility
    {
        private readonly IScsClient _client;
        private readonly RequestReplyMessenger<IScsClient> _clientRequestReply;

        public ConnectionUtility(IScsClient client, RequestReplyMessenger<IScsClient> clientRequestReply)
        {
            _client = client;
            _clientRequestReply = clientRequestReply;
        }

        public void SendMessage(string command, params string[] parameters)
        {
            SetConnectionIfDisconnected();

            _client.SendMessage(
                new ScsTextMessage(MessageText(command, parameters)));
        }

        public string SendMessageAndWaitForResponse(string command, params string[] parameters)
        {
            SetConnectionIfDisconnected();

            var resp = (ScsTextMessage) _clientRequestReply.SendMessageAndWaitForResponse(
                new ScsTextMessage(MessageText(command, parameters)));
            return resp == null ? string.Empty : resp.Text;
        }

        private string MessageText(string command, params string[] parameters)
        {
            var text = new StringBuilder();

            text.Append(command);
            if (parameters.Length > 0)
                text.Append(parameters[0]);
            for (int i = 1; i < parameters.Length; i++)
                text.Append("$" + parameters[i]);
            
            return text.ToString();
        }

        private void SetConnectionIfDisconnected()
        {
            if (_client.CommunicationState == CommunicationStates.Disconnected)
                _client.Connect();
        }
    }
}