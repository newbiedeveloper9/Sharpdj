using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servertcp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();

            server.Start();
        }
    }
}
