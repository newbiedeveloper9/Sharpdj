using Communication.Server.Logic.Commands;
using Communication.Shared;

namespace Communication.Server.Logic
{
    public class Register : ICommand
    {
        public string CommandText { get; } = Commands.Instance.CommandsDictionary["Register"];
        
        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}