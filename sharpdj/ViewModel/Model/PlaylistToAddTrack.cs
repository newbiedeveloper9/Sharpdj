using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;

namespace SharpDj.ViewModel.Model
{
    public class PlaylistToAddTrack : BaseViewModel
    {
        #region .ctor
        public PlaylistToAddTrack(SdjMainViewModel main, string playlistName, int trackcount)
        {
            SdjMainViewModel = main;
            PlaylistName = playlistName;
            TrackCount = trackcount;
        }
        #endregion .ctor

        #region Properties
        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
            }
        }


        private string _playlistName;
        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                if (_playlistName == value) return;
                _playlistName = value;
                OnPropertyChanged("PlaylistName");
            }
        }


        private int _trackCount;
        public int TrackCount
        {
            get => _trackCount;
            set
            {
                if (_trackCount == value) return;
                _trackCount = value;
                OnPropertyChanged("TrackCount");
            }
        }



        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands


        #endregion Commands


    }
}
