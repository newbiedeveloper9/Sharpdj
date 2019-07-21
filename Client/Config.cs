using Caliburn.Micro;
using Newtonsoft.Json;
using SCPackets.Models;
using SharpConfig;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System;
using System.IO;
using System.Threading;

namespace SharpDj
{
    public class Config : 
        IHandle<IPlaylistCollectionChanged>,
        IHandle<INewPlaylistCreated>,
        IHandle<INickColorChanged>,
        IHandle<IAuthKeyPublish>, IHandle<INotLoggedIn>
    {
        private BindableCollection<PlaylistModel> Playlists { get; set; }
            = new BindableCollection<PlaylistModel>();

        private readonly IEventAggregator _eventAggregator;

        public int Port { get; set; } = 5666;
        public string Ip { get; set; } = "192.168.0.103";

        public string AuthenticationKey { get; set; } = "";

        private ColorModel _nickColor = new ColorModel(147, 112, 219);
        public ColorModel NickColor
        {
            get => _nickColor;
            set
            {
                if (_nickColor == value) return;
                _nickColor = value;

                _eventAggregator.PublishOnUIThread(
                    new NickColorChanged(value));

                var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);
                if (value.ToString().Equals(
                    new ColorModel(config["Settings"]["NickColor"].StringValue).ToString())) return;
                Build();
            }
        }


        public Config(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        /// <returns>True if loaded correctly</returns>
        public bool LoadCore()
        {
            try
            {
                var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);

                Ip = config["Connection"]["IP"].StringValue;
                Port = config["Connection"]["Port"].IntValue;
                AuthenticationKey = config["Connection"]["AuthenticationKey"].StringValue;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Configuration file error.\n{e.Message}");
                return false;
            }
        }

        /// <returns>True if loaded correctly</returns>
        public bool LazyLoad()
        {
            try
            {
                var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);

                NickColor = new ColorModel(config["Settings"]["NickColor"].StringValue);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Configuration file error.\n{e.Message}");
                return false;
            }
        }

        public Config BuildIfNotExist()
        {
            if (File.Exists(FilesPath.Config.ConfigFile)) return this;

            Build();
            return this;
        }

        public void Build()
        {
            var config = new Configuration();
            config["Connection"]["IP"].StringValue = Ip;
            config["Connection"]["Port"].IntValue = Port;
            config["Connection"]["AuthenticationKey"].StringValue = AuthenticationKey;
            config["Settings"]["NickColor"].StringValue = NickColor.ToString();
            config.SaveToFile(FilesPath.Config.ConfigFile);
        }

        #region Playlist
        public void SavePlaylist()
        {
            if (!File.Exists(FilesPath.Config.PlaylistFile))
            {
                File.Create(FilesPath.Config.PlaylistFile);
                return;
            }

            string fileString = File.ReadAllText(FilesPath.Config.PlaylistFile);
            string json = JsonConvert.SerializeObject(Playlists, Formatting.Indented);

            if (fileString.Equals(json))
                return;

            File.WriteAllText(FilesPath.Config.PlaylistFile, json);
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
        #endregion Playlist

        public void Handle(IPlaylistCollectionChanged message)
        {
            Playlists = new BindableCollection<PlaylistModel>(message.PlaylistCollection);
            SavePlaylist();
        }

        public void Handle(INewPlaylistCreated message)
        {
            Playlists.Add(message.Model);
            SavePlaylist();
        }

        public void Handle(INickColorChanged message)
        {
            NickColor = message.Color;
        }

        public void Handle(IAuthKeyPublish message)
        {
            AuthenticationKey = message.AuthKey;

            var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);
            config["Connection"]["AuthenticationKey"].StringValue = AuthenticationKey;
            config.SaveToFile(FilesPath.Config.ConfigFile);
        }

        public void Handle(INotLoggedIn message)
        {
            if (string.IsNullOrWhiteSpace(AuthenticationKey)) return;

            var config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);
            config["Connection"]["AuthenticationKey"].StringValue = string.Empty;
            config.SaveToFile(FilesPath.Config.ConfigFile);
        }
    }
}
 