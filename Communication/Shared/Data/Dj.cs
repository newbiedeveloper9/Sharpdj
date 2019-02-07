using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Shared
{
    public class Dj
    {
        public Dj()
        {
            Track = new List<Song>();
        }

        public Dj(string host, List<Song> track)
        {
            Host = host;
            Track = track;
        }

        public string Host { get; set; }
        public List<Song> Track { get;set; }

        public class Song
        {
            //TODO create enum with type of media (yt, soundcloud, etc.)
            public Song(int time, string id)
            {
                Time = time;
                Id = id;
            }

            public int Time;
            public string Id;
        }
    }
}
