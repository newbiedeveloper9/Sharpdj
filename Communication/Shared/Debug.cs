using System;
using System.IO;

namespace Communication.Shared
{
    public class Debug
    {
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
                File.AppendAllText("log", content + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
