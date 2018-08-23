using System.Text;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace Communication.Server.Logic
{
    public class ConnectionUtility
    {
        private readonly IScsServerClient _client;

        public ConnectionUtility(IScsServerClient client)
        {
            _client = client;
        }

        public void SendMessage(string command, params string[] parameters)
        {
            _client.SendMessage(
                new ScsTextMessage(MessageText(command, parameters)));
        }

        public void ReplyToMessage(string command, string messageId, params string[] parameters)
        {
            _client.SendMessage(
                new ScsTextMessage(MessageText(command, parameters), messageId));
        }

        private string MessageText(string command, params string[] parameters)
        {
            var text = new StringBuilder();

            text.Append($"{command} ");
            if (parameters.Length > 0)
                text.Append(parameters[0]);
            for (int i = 1; i < parameters.Length; i++)
                text.Append("$" + parameters[i]);

            return text.ToString();
        }
    }
}