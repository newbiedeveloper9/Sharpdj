using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using Caliburn.Micro;
using SharpConfig;

namespace SharpDj
{
    public class Config
    {
        private readonly IEventAggregator _eventAggregator;


        public int Port { get; set; } = 21337;
        public string Ip { get; set; } = "192.168.0.103";


        public Config(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool Load(string configFile = "config.cfg")
        {
            try
            {
                var config = Configuration.LoadFromFile(configFile);

                Ip = config["Connection"]["IP"].StringValue;
                Port = config["Connection"]["Port"].IntValue;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Configuration file error.\n{e.Message}");
                return false;
            }
        }

        public Config BuildIfNotExist(string configFile = "config.cfg")
        {
            if (File.Exists(configFile)) return this;

            var config = new Configuration();
            config["Connection"]["IP"].StringValue = Ip;
            config["Connection"]["Port"].IntValue = Port;
            config.SaveToFile(configFile);

            return this;
        }
    }
}
