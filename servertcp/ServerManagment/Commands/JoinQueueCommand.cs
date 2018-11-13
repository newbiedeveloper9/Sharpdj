using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Server;
using Communication.Server.Logic;
using Communication.Shared;
using Hik.Communication.Scs.Server;
using Newtonsoft.Json;
using YoutubeExplode;

namespace servertcp.ServerManagment.Commands
{
    public class JoinQueueCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["JoinQueue"];

        public async void Run(IScsServerClient client, List<string> parameters, string messageId)
        {
            var sender = new ServerSender(client);
            var dj = JsonConvert.DeserializeObject<Dj>(parameters[0]);

            if (!Utils.Instance.IsActiveLogin(client) || dj.Track.Count <= 0)
                sender.Error(messageId);

            foreach (var track in dj.Track)
            {
                if (!YoutubeClient.ValidateVideoId(track.Id))
                {
                    sender.Error(messageId);
                    return;
                }
            }

            var userClient = DataSingleton.Instance.ServerClients[(int) client.ClientId].ToUserClient();
            
            //TODO to change if want to have multichannel watchning feature
            var room = DataSingleton.Instance.Rooms.GetAllItems()
                .FirstOrDefault(x => x.InsideInfo.Clients.Exists(
                    y => y.Id == userClient.Id));
            if (room == null)
            {
                sender.Error(messageId);
                return;
            }
            
            var yt = new YoutubeClient();
            foreach (var track in dj.Track)
            {
                var query = await yt.GetVideoAsync(track.Id);
                track.Time = Convert.ToInt32(query.Duration.TotalSeconds);
            }

            room.InsideInfo.Djs.Add(dj);
            sender.Success(messageId);
            
            if (room.InsideInfo?.Clients != null)
                foreach (var roomUser in room.InsideInfo?.Clients)
                {
                    var roomClient = DataSingleton.Instance.ServerClients.GetAllItems()
                        .FirstOrDefault(x => x.Id == roomUser.Id)
                        ?.Client;
                    
                    sender = new ServerSender(roomClient);
                    sender.NewDjInQueue(dj);
                }
        }
    }
}