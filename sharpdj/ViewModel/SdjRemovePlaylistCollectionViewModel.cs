using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.ViewModel
{
    class SdjRemovePlaylistCollectionViewModel : BaseViewModel
    {

        #region .ctor

        public SdjRemovePlaylistCollectionViewModel(SdjMainViewModel main)
        {
            
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
        }
        #endregion

        #endregion Commands


    }
}
