using System;
using System.IO;
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
            var path = FilesPath.Instance.ClientConfig;

            if (!File.Exists(path))
            {

                var json = JsonConvert.SerializeObject(new ClientConfig(), Formatting.Indented);
                File.WriteAllText(path, json);
            }

            try
            {
                var obj = JsonConvert.DeserializeObject<ClientConfig>(
                    File.ReadAllText(path));
                if (obj != null)
                {

                }
                else
                    throw new Exception("Config is empty");
                return obj;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}