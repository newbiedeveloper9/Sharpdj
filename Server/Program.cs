namespace Server
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