using System;
using System.IO;
using Communication.Shared;
using Newtonsoft.Json;

namespace SharpDj.Logic.Helpers
{
    public class ClientConfig
    {
        public int Port { get; set; } = 21337;
        public string Ip { get; set; } = "192.168.0.103";
        public int RefreshDataDelay { get; set; } = 20000;

        private ClientConfig()
        {
        }

        public static ClientConfig LoadConfig()
        {
            var debug = new Debug("Config");    
            var path = FilesPath.Instance.ClientConfig;

            if (!File.Exists(path))
            {

                var json = JsonConvert.SerializeObject(new ClientConfig(), Formatting.Indented);
                File.WriteAllText(path, json);
                debug.Log("Created new config file.");
            }

            try
            {
                var obj = JsonConvert.DeserializeObject<ClientConfig>(
                    File.ReadAllText(path));
                if (obj != null)
                    debug.Log("Success deserialize");
                else
                    throw new Exception("Config is null");
                return obj;
            }
            catch (Exception ex)
            {
                debug.Log("Error deserialize");
                debug.Log(ex.Message);
            }

            return null;
        }
    }
}