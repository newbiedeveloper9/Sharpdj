using System;
using System.IO;
using System.Reflection;

namespace SharpDj.Logic.Helpers
{
    public sealed class FilesPath
    {
        public readonly string ConfigFolder;
        public readonly string MusicFolder;
        
        public static readonly string LocalPath = Path.GetDirectoryName(
            Assembly.GetEntryAssembly().Location);


        private static Lazy<FilesPath> lazy =
            new Lazy<FilesPath>(() => new FilesPath());

        public static FilesPath Instance => lazy.Value;

        private FilesPath()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ConfigFolder = $@"{appdata}\sharpdj\config\";
            MusicFolder = $@"{LocalPath}\music\";
        }

        public string PlaylistConfig =>
           $"{ConfigFolder}playlist.json";

        public string ClientConfig =>
            $"{ConfigFolder}config.json";

    }
}