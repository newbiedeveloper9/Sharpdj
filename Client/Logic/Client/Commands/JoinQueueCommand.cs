using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.Logic.Helpers;
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
            Console.WriteLine(FilesPath.Instance.MusicFolder);
            var converter = new YoutubeConverter();
            foreach (var track in dj.Track)
            {
                if (File.Exists($@"{FilesPath.Instance.MusicFolder}{track.Id}.mp4")) continue;
                await converter.DownloadVideoAsync(track.Id, $@"{FilesPath.Instance.MusicFolder}{track.Id}.mp4");
            }
        }
    }
}