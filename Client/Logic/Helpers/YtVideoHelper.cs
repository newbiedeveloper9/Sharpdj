using SharpDj.Models;
using YoutubeExplode.Videos;

namespace SharpDj.Logic.Helpers
{
    public class YtVideoHelper
    {
/*        public static string NormalizeVideoId(string input)
        {
            return YoutubeClient.TryParseVideoId(input, out var videoId)
                ? videoId
                : input;
        }*/

        public static TrackModel ToTrackModel(Video video)
        {
            return new TrackModel()
            {
                Author = video.Author,
                Name = video.Title,
                ImgSource = video.Thumbnails.LowResUrl,
                TrackLink = video.Id,
                Duration = video.Duration,
            };
        }
    }
}
