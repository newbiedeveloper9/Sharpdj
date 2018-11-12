using System;
using System.IO;

namespace SharpDj.Logic.Helpers
{
    public sealed class FilesPath
    {
        public readonly string ConfigFolder;

        private static Lazy<FilesPath> lazy =
            new Lazy<FilesPath>(() => new FilesPath());

        public static FilesPath Instance => lazy.Value;

        private FilesPath()
        {
            ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sharpdj/config/";
        }


        public string PlaylistConfig =>
           $"{ConfigFolder}playlist.json";

        public string ClientConfig =>
            $"{ConfigFolder}config.json";
    }
}