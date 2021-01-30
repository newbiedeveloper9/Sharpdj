using Caliburn.Micro;
using Newtonsoft.Json;
using SCPackets.Models;
using SharpConfig;
using SharpDj.Logic.Helpers;
using SharpDj.Models;
using SharpDj.PubSubModels;
using System;
using System.IO;
using Serilog;

namespace SharpDj
{
    public class Config :
        IHandle<IPlaylistCollectionChanged>,
        IHandle<INewPlaylistCreated>,
        IHandle<INickColorChanged>,
        IHandle<IAuthKeyPublish>, IHandle<INotLoggedIn>
    {
        private Configuration _config;

        private BindableCollection<PlaylistModel> Playlists { get; set; }
            = new BindableCollection<PlaylistModel>();

        private readonly IEventAggregator _eventAggregator;

        public int Port { get; set; } = 5666;
        public string Ip { get; set; } = "192.168.0.103";
        public string AuthenticationKey { get; set; } = "";

        private Color _nickColor = new Color().SetColor(new byte[] { 147, 112, 219 });
        public Color NickColor
        {
            get => _nickColor;
            set
            {
                if (_nickColor.ToString() == value.ToString())
                {
                    return;
                }

                _nickColor = value;
                _eventAggregator.PublishOnUIThread(new NickColorChanged(value));

                Build();
            }
        }


        public Config(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _config = new Configuration();

            if (!File.Exists(FilesPath.Config.ConfigFile))
            {
                Build();
            }
        }

        /// <returns>True if loaded correctly</returns>
        public bool LoadCore()
        {
            try
            {
                _config = Configuration.LoadFromFile(FilesPath.Config.ConfigFile);

                Ip = _config["Connection"]["IP"].StringValue;
                Port = _config["Connection"]["Port"].IntValue;
                AuthenticationKey = _config["Connection"]["AuthenticationKey"].StringValue;

                return true;
            }
            catch (Exception e)
            {
                Log.Error("An error occurred while loading a config file. {@Error}", e.StackTrace);
                return false;
            }
        }

        /// <returns>True if loaded correctly</returns>
        public bool LazyLoad()
        {
            try
            {
                var nickColor = _config["Settings"]["NickColor"].StringValue.Remove(0, 1);
                NickColor = new Color().SetColor(nickColor);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Configuration file error.\n{e.Message}");
                return false;
            }
        }

        public void Build()
        {
            _config["Connection"]["IP"].StringValue = Ip;
            _config["Connection"]["Port"].IntValue = Port;
            _config["Connection"]["AuthenticationKey"].StringValue = AuthenticationKey;
            _config["Settings"]["NickColor"].StringValue = NickColor.ToString();
            _config.SaveToFile(FilesPath.Config.ConfigFile);
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

        #region Handles
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

            _config["Connection"]["AuthenticationKey"].StringValue = AuthenticationKey;
            _config.SaveToFile(FilesPath.Config.ConfigFile);
        }

        public void Handle(INotLoggedIn message)
        {
            if (string.IsNullOrWhiteSpace(AuthenticationKey)) return;

            _config["Connection"]["AuthenticationKey"].StringValue = string.Empty;
            _config.SaveToFile(FilesPath.Config.ConfigFile);
        }
        #endregion Handles
    }
}
