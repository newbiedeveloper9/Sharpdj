using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.Models.Helpers
{
    class Imgur
    {
        public string ApiKey { get; set; } = "3f25bffad37f3b8";

        public void AnonymousImageUpload(string path, bool local = true)
        {
            try
            {
                using (var w = new WebClient())
                {
                    NameValueCollection values;
                    if (local)
                    {
                        values = new NameValueCollection
                        {
                            {"image", Convert.ToBase64String(File.ReadAllBytes(path))}
                        };
                    }
                    else
                    {
                        values = new NameValueCollection
                        {
                            {"image", path}
                        };
                    }

                    w.Headers.Add("Authorization", "Client-ID " + ApiKey);
                    var response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);
                    var responseString = Encoding.UTF8.GetString(response);

                    Console.WriteLine(responseString);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Console.WriteLine(error);
            }
        }
    }
}