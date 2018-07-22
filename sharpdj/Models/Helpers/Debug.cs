using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpDj.Models.Helpers
{
    class Debug
    {
        public string WindowTitle { get; set; }

        public Debug(string windowTitle)
        {
            WindowTitle = windowTitle;
        }

        public Debug()
        {

        }

        public void Log(string message)
        {
            var content = $"{DateTime.Now}    [{WindowTitle}]: {message}";
            log(content);
        }

        public static void Log(string windowTitle, string message)
        {
            var content = $"{DateTime.Now}    [{windowTitle}]: {message}";
            log(content);
        }

        private static void log(string content)
        {
            Console.WriteLine(content);
            File.AppendAllText("log", content+Environment.NewLine);
        }
    }
}
