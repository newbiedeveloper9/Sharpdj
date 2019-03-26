using Caliburn.Micro;
using Newtonsoft.Json;
using SharpConfig;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace SharpDj
{
    public class Config : IHandle<IPlaylistCollectionChanged>, IHandle<INewPlaylistCreated>
    {
        private BindableCollection<PlaylistModel> Playlists { get; set; }
            = new BindableCollection<PlaylistModel>();

        private readonly IEventAggregator _eventAggregator;

        public int Port { get; set; } = 21337;
        public string Ip { get; set; } = "192.168.0.103";


        public Config(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        /// <returns>True if loaded correctly</returns>
        public bool Load()
        {
            try
            {
                var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);

                Ip = config["Connection"]["IP"].StringValue;
                Port = config["Connection"]["Port"].IntValue;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Configuration file error.\n{e.Message}");
                return false;
            }
        }

        public void SavePlaylistWithDelay(int delay)
        {
            string fileString = File.ReadAllText(FilesPath.Config.PlaylistFile);
            string json = JsonConvert.SerializeObject(Playlists, Formatting.Indented);

            if (fileString.Equals(json))
                return;
        
            Thread.Sleep(delay);
            File.WriteAllText(FilesPath.Config.PlaylistFile, json);
            Console.WriteLine(FilesPath.Config.PlaylistFile);
        }

        /// <returns>True if loaded file, false if not exist or problem occured</returns>
        public bool LoadPlaylist()
        {
            try
            {
                if (!File.Exists(FilesPath.Config.PlaylistFile))
                    return false;

                var src = File.ReadAllText(FilesPath.Config.PlaylistFile);
                var json = JsonConvert.DeserializeObject<BindableCollection<PlaylistModel>>(src);

                if (json == null)
                    return false;

                _eventAggregator.PublishOnUIThread(new PlaylistCollectionChanged(json));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Loading playlist file error.\n{e.Message}");
                return false;
            }
        }

        public Config BuildIfNotExist()
        {
            if (File.Exists(FilesPath.Config.ConfigFile)) return this;

            var config = new Configuration();
            config["Connection"]["IP"].StringValue = Ip;
            config["Connection"]["Port"].IntValue = Port;
            config.SaveToFile(FilesPath.Config.ConfigFile);

            return this;
        }

        public void Handle(IPlaylistCollectionChanged message)
        {
            Playlists = new BindableCollection<PlaylistModel>(message.PlaylistCollection);
            SavePlaylistWithDelay(0);
        }

        public void Handle(INewPlaylistCreated message)
        {
            Playlists.Add(message.Model);
            SavePlaylistWithDelay(0);
        }

    }
}
