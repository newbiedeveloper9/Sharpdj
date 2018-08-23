using System;

namespace SharpDj.Logic.Helpers
{
    public sealed class FilesPath
    {
        private readonly string _configFolder = Environment.CurrentDirectory + @"\config\";

        private static Lazy<FilesPath> lazy =
            new Lazy<FilesPath>(() => new FilesPath());

        public static FilesPath Instance => lazy.Value;

        private FilesPath()
        {
        }


        public string PlaylistConfig =>
           $"{_configFolder}playlist.json";

        public string ClientConfig =>
            $"{_configFolder}config.json";
    }
}