using System.Collections.Generic;
using System.Linq;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.ViewModel;
using YoutubeExplode;

namespace SharpDj.Logic.Client.Commands
{
    public class UpdateDjCommand : ICommand
    {
        public string CommandText { get; } = Communication.Shared.Commands.Instance.CommandsDictionary["UpdateDj"];
        
        public void Run(SdjMainViewModel sdjMainViewModel, List<string> parameters)
        {
            var json = parameters[0];
            
            var inside = JsonConvert.DeserializeObject<Room.InsindeInfo>(json);

          //  sdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = inside.Clients.Count;
            sdjMainViewModel.SdjBottomBarViewModel.BottomBarSizeOfPlaylistInRoom = inside.Djs.Count;
            sdjMainViewModel.SdjBottomBarViewModel.BottomBarMaxSizeOfPlaylistInRoom = 30;
            sdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfAdministrationInRoom =
                inside.Clients.Count(x => x.Rank > 0);
            sdjMainViewModel.SdjRoomViewModel.SongsQueue = (sbyte)inside.Djs.SelectMany(dj => dj.Video).Count();

            var client = new YoutubeClient();
            var tmp = client.GetVideoAsync(inside.Djs[0].Video[0].Id).Result;
            sdjMainViewModel.SdjRoomViewModel.SongTitle = tmp.Title;
            sdjMainViewModel.SdjBottomBarViewModel.BottomBarTitleOfActuallySong = tmp.Title;
        }
    }
}