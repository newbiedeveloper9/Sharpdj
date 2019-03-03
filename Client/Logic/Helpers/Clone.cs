using Newtonsoft.Json;

namespace SharpDj.Logic.Helpers
{
    class Clone
    {
        public static T ClonWithJson<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
