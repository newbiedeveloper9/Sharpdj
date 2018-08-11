using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;

namespace SharpDj.Logic.Helpers
{
    public sealed class YoutubeSingleton
    {
        private static readonly Lazy<YoutubeSingleton> lazy =
            new Lazy<YoutubeSingleton>(() => new YoutubeSingleton());

        public static YoutubeSingleton Instance => lazy.Value;

        private YoutubeSingleton()
        {

        }

        public YoutubeClient YtClient { get; set; } = new YoutubeClient();
    }
}
