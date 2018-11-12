using System;
using System.Collections.Generic;
using System.Reflection;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.ViewModel;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Models.MediaStreams;

namespace SharpDj.Logic.Client.Commands
{
    public class ChangeTrackCommand : ICommand
    {
        public string CommandText { get; } =
            Communication.Shared.Commands.Instance.CommandsDictionary["ChangeTrack"];
        
        public async void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var json = JsonConvert.DeserializeObject<Dj>(parameters[0]);
            var roomId = parameters[1];

            Console.WriteLine(json);
            var location = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var converter = new YoutubeConverter();
            await converter.DownloadVideoAsync(json.Track[0].Id, $@"{location}\{json.Track[0].Id}.mp4");
           
            sdjMainViewModel.SdjRoomViewModel.VlcPlayer.VlcPlayer.SourceProvider.MediaPlayer.SetMedia(
                new Uri(location+$@"\{json.Track[0].Id}.mp4"));
            sdjMainViewModel.SdjRoomViewModel.VlcPlayer.VlcPlayer.SourceProvider.MediaPlayer.Play();
        }
    }
}