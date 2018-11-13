using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.ViewModel;
using YoutubeExplode.Converter;

namespace SharpDj.Logic.Client.Commands
{
    public class JoinQueueCommand : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["JoinQueue"];
        
        public async void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var dj = JsonConvert.DeserializeObject<Dj>(parameters[0]);

            var converter = new YoutubeConverter();
            var location = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            foreach (var track in dj.Track)
            {
                if (File.Exists($@"{location}\music\{track.Id}.mp4")) continue;
                await converter.DownloadVideoAsync(track.Id, $@"{location}\music\{track.Id}.mp4");
            }
        }
    }
}