using System.IO;

namespace SharpDj.Logic.Helpers
{
    public class ExecuteOnStart
    {
        public ExecuteOnStart()
        {
            if (!Directory.Exists(FilesPath.Instance.ConfigFolder))
                Directory.CreateDirectory(FilesPath.Instance.ConfigFolder);
        }
    }
}