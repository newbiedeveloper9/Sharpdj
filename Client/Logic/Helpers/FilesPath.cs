using System;
using System.IO;
using System.Reflection;

namespace SharpDj.Logic.Helpers
{
    public sealed class FilesPath
    {
        //Folders
        public readonly string AppdataDirectory;
        public readonly string MusicDirectory;
        public readonly string LocalDirectory;

        //Configs
        public readonly string PlaylistFile;
        public readonly string ConfigFile;
        public readonly string DebugFile;


        private static Lazy<FilesPath> lazy =
            new Lazy<FilesPath>(() => new FilesPath());

        public static FilesPath Config => lazy.Value;

        private FilesPath()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            //Folders
            AppdataDirectory = $@"{appdata}\SharpDJ";
            LocalDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            MusicDirectory = $@"{LocalDirectory}\music\";

            //Configs
            PlaylistFile = $@"{AppdataDirectory}\playlist.json";
            ConfigFile = $@"{AppdataDirectory}\config.ini";
            DebugFile = $@"{LocalDirectory}\debug.txt";
        }
    }
}