using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.XPath;
using Debug = Communication.Shared.Debug;

namespace SharpDj.Logic.Helpers
{
    class Imgur
    {
        public string ApiKey { get; set; } = "3f25bffad37f3b8";

        public string AnonymousImageUpload(string path, bool local = true)
        {
            XmlDocument doc = new XmlDocument();
            var debug = new Debug("Imgur");
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
                    debug.Log("Uploading image");
                    var response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);
                    var responseString = Encoding.UTF8.GetString(response);

                    doc.LoadXml(responseString);

                    if (doc.ChildNodes[1].Attributes[2].InnerText.Equals("200"))
                    {
                        debug.Log("Success");
                        return doc.DocumentElement.ChildNodes.Item(26).InnerText;
                    }

                    debug.Log("Error with xml parsing");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                new ExceptionLogger(ex);
                return string.Empty;
            }
        }
    }
}