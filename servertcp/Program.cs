using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;
using Newtonsoft.Json;
using servertcp.Sql;

namespace servertcp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerManagment.Server();

            server.Start();
        }
    }
}