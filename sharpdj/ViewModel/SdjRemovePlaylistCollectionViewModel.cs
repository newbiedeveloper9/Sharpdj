using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.ViewModel.Helpers;

namespace SharpDj.ViewModel
{
    public class SdjRemovePlaylistCollectionViewModel : BaseViewModel
    {

        #region .ctor

        public SdjRemovePlaylistCollectionViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, closeForm);
            SdjTitleBarForUserControlsViewModel.FormName = "Remove Playlist";
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, closeForm);
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

        private SdjTitleBarForUserControlsViewModel _sdjTitleBarForUserControlsViewModel;
        public SdjTitleBarForUserControlsViewModel SdjTitleBarForUserControlsViewModel
        {
            get => _sdjTitleBarForUserControlsViewModel;
            set
            {
                if (_sdjTitleBarForUserControlsViewModel == value) return;
                _sdjTitleBarForUserControlsViewModel = value;
                OnPropertyChanged("SdjTitleBarForUserControlsViewModel");
            }
        }

        private SdjBackgroundForFormsViewModel _sdjBackgroundForFormsViewModel;
        public SdjBackgroundForFormsViewModel SdjBackgroundForFormsViewModel
        {
            get => _sdjBackgroundForFormsViewModel;
            set
            {
                if (_sdjBackgroundForFormsViewModel == value) return;
                _sdjBackgroundForFormsViewModel = value;
                OnPropertyChanged("SdjBackgroundForFormsViewModel");
            }
        }

        #endregion Properties

        #region Methods

        private void closeForm()
        {
            SdjMainViewModel.PlaylistStateCollectionVisibility = PlaylistState.Collapsed;
        }

        #endregion Methods

        #region Commands

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
            closeForm();
        }
        #endregion

        #endregion Commands


    }
}
