using System.Collections.Generic;
using Hik.Communication.Scs.Server;

namespace servertcp.ServerManagment.Commands
{
    public interface ICommand
    {
        string CommandText { get; }
        void Run(IScsServerClient client, List<string> parameters, string messageId);
    }
}