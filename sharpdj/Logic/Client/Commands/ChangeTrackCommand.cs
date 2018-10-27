using System.Collections.Generic;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.ViewModel;

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
            
            sdjMainViewModel.SdjRoomViewModel.VlcPlayer.
                VlcPlayer.SourceProvider.MediaPlayer.Play();
        }
    }
}