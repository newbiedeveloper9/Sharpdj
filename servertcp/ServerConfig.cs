using System;
using System.IO;
using Newtonsoft.Json;

namespace servertcp
{
    public class ServerConfig
    {
        public int Port { get; set; } = 21337;
        public string Ip { get; set; }
        public bool Whitelist { get; set; } = false;
        public bool Blacklist { get; set; } = false;

        private ServerConfig()
        {
        }

        public static ServerConfig LoadConfig(string configFile = "config.json")
        {
            var path = $"{Environment.CurrentDirectory}\\{configFile}";

            if (!File.Exists(path))
            {
                var json = JsonConvert.SerializeObject(new ServerConfig(), Formatting.Indented);
                File.WriteAllText(path, json);
                Console.WriteLine("Created new config file. Complete server configuration file and restart.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            while (true)
                try
                {
                    return JsonConvert.DeserializeObject<ServerConfig>(
                        File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in load config:");
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
        }
    }
}