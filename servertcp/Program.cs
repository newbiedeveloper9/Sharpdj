using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using servertcp.Sql;

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
