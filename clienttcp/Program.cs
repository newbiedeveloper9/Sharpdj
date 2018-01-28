using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using clienttcp;
using clienttcp.Properties;
using Communication.Client;
using Communication.Shared;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;

namespace clienttcp
{
    public class Client
    {
        private ClientSender _sender;
        private ClientReceiver _receiver;

        private IScsClient client;
        public string Ip { get; set; } = "78.88.84.56";
        public int Port { get; set; } = 21007;

        public void Start()
        {

            _sender = new ClientSender();

            client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Ip, Port));
            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Client_Disconnected;
            client.Connect();
            Console.WriteLine("Connected with server!");
            while (true)
            {
                Thread.Sleep(500);
                Console.WriteLine();
                Console.WriteLine();
                var command = Console.ReadLine();
                if (client.CommunicationState == CommunicationStates.Disconnected)
                    client.Connect();

                if (command.Equals("register"))
                {
                    Console.WriteLine("Login: ");
                    var login = Console.ReadLine();
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    Console.Write("Email: ");
                    var email = Console.ReadLine();
                    _sender.Register(client, login, password, email);
                }
                else if (command.Equals("login"))
                {
                    Console.WriteLine("Login: ");
                    var login = Console.ReadLine();
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    _sender.Login(client, login, password);
                }
                else if (command.Equals("disconnect"))
                {
                    _sender.Disconnect(client);
                }
                else if (command.Equals("connect"))
                {
                    client.Connect();
                }
                else if (command.Equals("changepassword"))
                {
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    Console.Write("New Password: ");
                    var newPassword = Console.ReadLine();

                    _sender.ChangePassword(client, password, newPassword);
                }
                else if (command.Equals("changerank"))
                {
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    foreach (var rank in Enum.GetNames(typeof(Rank)))
                    {
                        Console.WriteLine(rank);
                    }
                    Console.Write("Rank: ");
                    var newRank = Console.ReadLine();

                    _sender.ChangeRank(client, password, (Rank)Enum.Parse(typeof(Rank), newRank, true));
                }
                else if (command.Equals("changeusername"))
                {
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    Console.Write("New username: ");
                    var newUsername = Console.ReadLine();

                    _sender.ChangeUsername(client, password, newUsername);
                }
                else if (command.Equals("changelogin"))
                {
                    Console.Write("Password: ");
                    var password = Console.ReadLine();
                    Console.Write("New login: ");
                    var newLogin = Console.ReadLine();

                    _sender.ChangeLogin(client, password, newLogin);
                }
                else if (command.Equals("getpeoples"))
                {
                    _sender.GetPeoples(client);
                }
                else if (command.Equals(""))
                {
                    
                }
                else
                {
                    if (client.CommunicationState == CommunicationStates.Connected)
                        client.SendMessage(new ScsTextMessage(command));
                    else
                    {
                        Console.WriteLine("Press enter to reconnect");
                    }
                }
                //  Console.Clear();
            }

            Console.WriteLine("Enter key to disconnect");
            Console.ReadLine();
            client.Disconnect();
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {

        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;

            if (message == null)
                return;

            _receiver = new ClientReceiver();
            _receiver.ParseMessage(client, message.Text);
            //TODO: disconnected player
        }
    }


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
