using System.Collections.Generic;
using Hik.Communication.Scs.Server;

namespace Communication.Server.Logic.Commands
{
    public interface ICommand
    {
        string CommandText { get; }
        void Run(IScsServerClient client, List<string> parameters, string messageId);
    }
}