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
        public Room()
        {

        }

        public Room(int id, string name, string host, string image, string description)
        {
            Id = id;
            Name = name;
            Host = host;
            Image = image;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int AmountOfPeople { get; set; }
        public int AmountOfAdministration { get; set; }

        [JsonIgnore]
        public InsindeInfo InsideInfo { get; set; }


        public class InsindeInfo
        {
            private readonly Room _room;
            private List<UserClient> _clients;

            public InsindeInfo()
            {
                
            }

            public InsindeInfo(List<UserClient> clients, List<Songs> djs, Room room, int timeLeft = 0)
            {
                _room = room;
                Clients = clients;
                Djs = djs;
                TimeLeft = timeLeft;
            }

            public InsindeInfo(List<UserClient> clients, List<Songs> djs, int timeLeft = 0)
            {
                Clients = clients;
                Djs = djs;
                TimeLeft = timeLeft;
            }

            public List<UserClient> Clients
            {
                get => _clients;
                set
                {
                    if (value == _clients) return;
                    _clients = value;
                    if(_room == null) return;
                    _room.AmountOfPeople = value.Count;
                    _room.AmountOfAdministration = value.Count(x => x.Rank > 0);
                }
            }

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
