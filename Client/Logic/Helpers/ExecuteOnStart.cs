using System.IO;
using YoutubeExplode.Converter;

namespace SharpDj.Logic.Helpers
{
    public class ExecuteOnStart
    {
        public ExecuteOnStart()
        {
            if (!Directory.Exists(FilesPath.Instance.ConfigFolder))
                Directory.CreateDirectory(FilesPath.Instance.ConfigFolder);
            if (!Directory.Exists(FilesPath.Instance.MusicFolder))
                Directory.CreateDirectory(FilesPath.Instance.MusicFolder);
        }
    }
}