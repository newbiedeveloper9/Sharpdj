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
            SdjTitleBarForUserControls = new SdjTitleBarForUserControls(main, closeForm);
            SdjTitleBarForUserControls.FormName = "Remove Playlist";
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

        private SdjTitleBarForUserControls _sdjTitleBarForUserControls;
        public SdjTitleBarForUserControls SdjTitleBarForUserControls
        {
            get => _sdjTitleBarForUserControls;
            set
            {
                if (_sdjTitleBarForUserControls == value) return;
                _sdjTitleBarForUserControls = value;
                OnPropertyChanged("SdjTitleBarForUserControls");
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
            closeForm();
        }
        #endregion


        #endregion Commands


    }
}
