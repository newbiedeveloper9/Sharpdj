using System.IO;
using YoutubeExplode.Converter;

namespace SharpDj.Logic.Helpers
{
    public class ExecuteOnStart
    {
        public ExecuteOnStart()
        {
            if (!Directory.Exists(FilesPath.Config.AppdataDirectory))
                Directory.CreateDirectory(FilesPath.Config.AppdataDirectory);
            if (!Directory.Exists(FilesPath.Config.MusicDirectory))
                Directory.CreateDirectory(FilesPath.Config.MusicDirectory);
        }
    }
}