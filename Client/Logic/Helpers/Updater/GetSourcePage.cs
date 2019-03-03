using System.Net.Http;

namespace SharpDj.Logic.Helpers.Updater
{
    class GetSourcePage
    {
        public static string GetSource(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        return content.ReadAsStringAsync().Result;
                    }
                }
            }
        }
    }
}
