using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Shared
{
   public class CommandsPacket
    {
        public const uint LoginAsGuest = 1;

        public class Succesfuls
        {
            public const uint SuccesfulLoginAsGuest = 2;
        }

        public class Errors
        {
            public const uint LoginAsGuestErr = 3;
        }
    }
}
