using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Communication.Shared;
using Communication.Shared.Data;
using Newtonsoft.Json;
using SharpDj.Logic.Helpers;
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

        public void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var json = JsonConvert.DeserializeObject<Dj>(parameters[0]);
            var roomId = parameters[1];

            Console.WriteLine(json);

            while (!File.Exists($@"{FilesPath.Instance.MusicFolder}{json.Track[0].Id}.mp4"))
            {
                Thread.Sleep(200);
            }
            
            Play:
            Thread.Sleep(300);
            try
            {
                
                sdjMainViewModel.SdjRoomViewModel.VlcPlayer.VlcPlayer.SourceProvider.MediaPlayer.SetMedia(
                    new Uri($@"{FilesPath.Instance.MusicFolder}{json.Track[0].Id}.mp4"));
                sdjMainViewModel.SdjRoomViewModel.VlcPlayer.VlcPlayer.SourceProvider.MediaPlayer.Play();
            }
            catch (Exception ex)
            {
                goto Play;
            }
        }
    }
}