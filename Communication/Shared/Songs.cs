using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Shared
{
    public class Songs
    {
        public Songs()
        {
            
        }

        public Songs(string host, List<Song> video)
        {
            Host = host;
            Video = video;
        }

        public string Host { get; set; }
        public List<Song> Video { get;set; }

        public class Song
        {
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
