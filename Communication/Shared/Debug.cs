using System;
using System.IO;
using System.Reflection;

namespace Communication.Shared
{
    public class Debug
    {
        private static readonly string LocalPath = Path.GetDirectoryName(
            Assembly.GetEntryAssembly().Location);
        
        public string WindowTitle { get; set; }

        public Debug(string windowTitle)
        {
            WindowTitle = windowTitle;
        }

        public void Log(string message)
        {
            var content = $"{DateTime.Now}    [{WindowTitle.ToUpper()}]: {message}";
            log(content);
        }

        public static void Log(string windowTitle, string message)
        {
            var content = $"{DateTime.Now}    [{windowTitle.ToUpper()}]: {message}";
            log(content);
        }

        private static void log(string content)
        {
            try
            {
                Console.WriteLine(content);
                File.AppendAllText(LocalPath + @"\log", content + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
