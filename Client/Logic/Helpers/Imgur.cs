using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.XPath;

namespace SharpDj.Logic.Helpers
{
    public class Imgur
    {
        public string ApiKey { get; set; } = "3f25bffad37f3b8";

        public string AnonymousImageUpload(string path, bool local = true)
        {
            XmlDocument doc = new XmlDocument();
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
                    Console.WriteLine("Uploading image");
                    var response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);
                    var responseString = Encoding.UTF8.GetString(response);

                    doc.LoadXml(responseString);

                    var xmlAttributeCollection = doc.ChildNodes[1].Attributes;
                    if (xmlAttributeCollection != null && xmlAttributeCollection[2].InnerText.Equals("200"))
                    {
                        if (doc.DocumentElement != null)
                        {
                            OnUploadSuccess(EventArgs.Empty);
                            Console.WriteLine("Upload Success");
                            return doc.DocumentElement.ChildNodes.Item(26)?.InnerText;
                        }
                    }

                    Console.WriteLine("Error with xml parsing");
                    OnXmlParsingError(EventArgs.Empty);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                new ExceptionLogger(ex);
                return string.Empty;
            }
        }

        public event EventHandler XmlParsingError;
        protected virtual void OnXmlParsingError(EventArgs e)
        {
            EventHandler handler = XmlParsingError;
            handler?.Invoke(this, e);
        }

        public event EventHandler UploadSuccess;
        protected virtual void OnUploadSuccess(EventArgs e)
        {
            EventHandler handler = UploadSuccess;
            handler?.Invoke(this, e);
        }
    }
}