using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Communication.Client.Logic;
using Communication.Shared;
using Communication.Shared.Data;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using Newtonsoft.Json;
using SharpDj.Enums.Menu;
using SharpDj.Enums.User;
using SharpDj.Logic.Helpers;
using SharpDj.ViewModel;
using SharpDj.ViewModel.Model;

namespace SharpDj.Logic.Client
{
    public class Client
    {
        public SdjMainViewModel SdjMainViewModel { get; set; }
        public ClientReceiver Receiver { get; set; }
        public ClientSender Sender { get; set; }
        public ClientConfig Config { get; } = ClientConfig.LoadConfig();
        private Debug _debug;

        public Client Start(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            Receiver = new ClientReceiver(main);
            _debug = new Debug("Connection");

            ClientInfo.Instance.Client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(Config.Ip, Config.Port));
            ClientInfo.Instance.Client.MessageReceived += Client_MessageReceived;
            ClientInfo.Instance.Client.Disconnected += Client_Disconnected;
            ClientInfo.Instance.ReplyMessenger = new RequestReplyMessenger<IScsClient>(ClientInfo.Instance.Client);
            ClientInfo.Instance.ReplyMessenger.Start();
          
            ClientInfo.Instance.Client.ConnectTimeout = 400;
          
            while (ClientInfo.Instance.Client.CommunicationState == CommunicationStates.Disconnected)
            {
                try
                {
#if DEBUG
                    Thread.Sleep(1000);
#endif
                    ClientInfo.Instance.Client.Connect();
                }
                catch (TimeoutException e)
                {
                    _debug.Log(e.Message);
                }
            }

            PeriodicTask.StartNew(80, RefreshData);

            Sender = new ClientSender(ClientInfo.Instance.Client, ClientInfo.Instance.ReplyMessenger);
            return this;
        }

        #region  Methods

        private void RefreshData()
        {
            if (ClientInfo.Instance.UserState != UserState.Logged) return;

            RefreshInfo();
            Thread.Sleep(Config.RefreshDataDelay);
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            _debug.Log("Disconnected");
            SdjMainViewModel.MainViewVisibility = MainView.Login;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;

            if (message == null)
                return;

            Debug.Log("Receiver", message.Text);
            Debug.Log("Message Id", message.RepliedMessageId);

            Receiver.ParseMessage(ClientInfo.Instance.Client, message.Text);
        }

        public void RefreshInfo()
        {
            Task.Factory.StartNew(() =>
            {
                string reply = SdjMainViewModel.Client.Sender.AfterLogin();
                if (reply == null) return;

                var source = JsonConvert.DeserializeObject<List<Room>>(reply);
                var roomstmp = new ObservableCollection<RoomSquareModel>();
                for (int i = 0; i < source.Count; i++)
                {
                    roomstmp.Add(new RoomSquareModel(SdjMainViewModel)
                    {
                        HostName = source[i].Host,
                        RoomName = source[i].Name,
                        AdminsInRoom = source[i].AmountOfAdministration,
                        PeopleInRoom = source[i].AmountOfPeople,
                        RoomDescription = source[i].Description,
                        RoomId = source[i].Id,
                    });
                }

                roomstmp = new ObservableCollection<RoomSquareModel>
                    (roomstmp.OrderByDescending(i => i.PeopleInRoom));
                SdjMainViewModel.RoomCollection = roomstmp;
            });
        }

        #endregion
    }
}