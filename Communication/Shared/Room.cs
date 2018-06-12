using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Communication.Shared
{
    public class Room
    {
        public Room(int id, string name, string host, string image, string description, InsindeInfo insideInfo)
        {
            Id = id;
            Name = name;
            Host = host;
            Image = image;
            Description = description;
            InsideInfo = insideInfo;
        }

        public int Id;
        public string Name;
        public string Host;
        public string Image;
        public string Description;
        public int AmountOfPeople => InsideInfo.Clients.Count;
        public int AmountOfAdministration => InsideInfo.Clients.Count(x => x.Rank > 0);

        public InsindeInfo InsideInfo;

        public class InsindeInfo
        {
            public InsindeInfo(List<UserClient> clients, List<Songs> djs, int timeLeft = 0)
            {
                Clients = clients;
                Djs = djs;
                TimeLeft = timeLeft;
            }

            public List<UserClient> Clients { get; set; }
            public List<Songs> Djs { get; set; }
            public int TimeLeft { get; set; }

            public void NextDj()
            {
                Djs[0].Video = Djs[0].Video.Skip(1).Concat(Djs[0].Video.Take(1)).ToList();
                Djs = Djs.Skip(1).Concat(Djs.Take(1)).ToList();
                TimeLeft = Djs[0].Video[0].Time;
            }
        }
    }
}
