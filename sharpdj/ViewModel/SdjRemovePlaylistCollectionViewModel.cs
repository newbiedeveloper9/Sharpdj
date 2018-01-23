using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;

namespace SharpDj.ViewModel
{
    public class SdjRemovePlaylistCollectionViewModel : BaseViewModel
    {

        #region .ctor

        public SdjRemovePlaylistCollectionViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
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
                OnPropertyChanged("SdjMainViewModel");
            }
        }


        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands

        #region CloseRemovePlaylistCommand
        private RelayCommand _closeRemovePlaylistCommand;
        public RelayCommand CloseRemovePlaylistCommand
        {
            get
            {
                return _closeRemovePlaylistCommand
                       ?? (_closeRemovePlaylistCommand = new RelayCommand(CloseRemovePlaylistCommandExecute, CloseRemovePlaylistCommandCanExecute));
            }
        }

        public bool CloseRemovePlaylistCommandCanExecute()
        {
            return true;
        }

        public void CloseRemovePlaylistCommandExecute()
        {
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Collapsed;
        }
        #endregion

        #region RemovePlaylistCommand
        private RelayCommand _removePlaylistCommand;
        public RelayCommand RemovePlaylistCommand
        {
            get
            {
                return _removePlaylistCommand
                       ?? (_removePlaylistCommand = new RelayCommand(RemovePlaylistCommandExecute, RemovePlaylistCommandCanExecute));
            }
        }

        public bool RemovePlaylistCommandCanExecute()
        {
            return true;
        }

        public void RemovePlaylistCommandExecute()
        {
            var item = SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.FirstOrDefault(x => x.IsSelected);
            if (item != null)
                SdjMainViewModel.SdjPlaylistViewModel.PlaylistCollection.Remove(item);
            CloseRemovePlaylistCommandExecute();
        }
        #endregion

        #region MouseUpAwayFromRemovePlaylistCommand
        private RelayCommand _mouseUpAwayFromRemovePlaylistCommand;
        public RelayCommand MouseUpAwayFromRemovePlaylistCommand
        {
            get
            {
                return _mouseUpAwayFromRemovePlaylistCommand
                       ?? (_mouseUpAwayFromRemovePlaylistCommand = new RelayCommand(MouseUpAwayFromRemovePlaylistCommandExecute, MouseUpAwayFromRemovePlaylistCommandCanExecute));
            }
        }

        public bool MouseUpAwayFromRemovePlaylistCommandCanExecute()
        {
            return true;
        }

        public void MouseUpAwayFromRemovePlaylistCommandExecute()
        {
            CloseRemovePlaylistCommandExecute();
        }
        #endregion


        #endregion Commands


    }
}
