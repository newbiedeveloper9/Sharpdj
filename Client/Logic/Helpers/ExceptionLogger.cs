using System;
using System.IO;

namespace SharpDj.Logic.Helpers
{
    class ExceptionLogger
    {
        public ExceptionLogger(Exception e)
        {
            Console.WriteLine($"Exception:{e.Source}", $"{e.Message}" );
            Console.WriteLine($"StackTrace", $"{e.StackTrace}" );
        }

        public ExceptionLogger(string e)
        {
            Console.WriteLine($"Exception: {e}");
            File.WriteAllText(FilesPath.Config.DebugFile, e);
        }
    }
}
