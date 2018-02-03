namespace clienttcp
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client;
            client = new Client();

            client.Start();
        }
    }
}
