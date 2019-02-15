using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.PubSubModels
{
    public class LoginRegisterAgentHandler : ILoginRegisterAgentHandler
    {
        public MoveTo MoveTo { get; set; }

        public LoginRegisterAgentHandler(MoveTo moveTo)
        {
            MoveTo = moveTo;
        }
    }

    public interface ILoginRegisterAgentHandler
    {
        MoveTo MoveTo { get; set; }
    }

    public enum MoveTo
    {
        Register,
        Login,
    }
}
