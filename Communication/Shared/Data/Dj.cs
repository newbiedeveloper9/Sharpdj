using System.Collections.Generic;

namespace Communication.Shared.Data
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
