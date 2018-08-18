using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpDj.Models.Helpers;

namespace SharpDj.Logic.Helpers
{
    public class ClientConfig
    {
        public int Port { get; set; } = 21337;
        public string Ip { get; set; } = "192.168.0.103";

        private ClientConfig()
        {
        }

        public static ClientConfig LoadConfig()
        {
            var _debug = new Debug("Config");
            var path = FilesPath.Instance.ClientConfig;

            if (!File.Exists(path))
            {
                var json = JsonConvert.SerializeObject(new ClientConfig(), Formatting.Indented);
                File.WriteAllText(path, json);
                _debug.Log("Created new config file.");
            }

            try
            {
                var obj = JsonConvert.DeserializeObject<ClientConfig>(
                    File.ReadAllText(path));
                if (obj != null)
                    _debug.Log("Success deserialize");
                else
                    throw new Exception("Config is null");
                return obj;
            }
            catch (Exception ex)
            {
                _debug.Log("Error deserialize");
                _debug.Log(ex.Message);
            }

            return null;
        }
    }
}