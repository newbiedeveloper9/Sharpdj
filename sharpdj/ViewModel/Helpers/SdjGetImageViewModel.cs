using Microsoft.Win32;
using SharpDj.Core;
using SharpDj.Enums.Helpers;

namespace SharpDj.ViewModel.Helpers
{
    public class SdjGetImageViewModel : BaseViewModel
    {
        public SdjGetImageViewModel(SdjMainViewModel sdjMainViewModel)
        {
            SdjMainViewModel = sdjMainViewModel;
            SdjTitleBarForUserControlsViewModel =
                new SdjTitleBarForUserControlsViewModel(sdjMainViewModel, CloseForm, "Get image");
            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(sdjMainViewModel, CloseForm);
        }

        #region Properties

        private string _localPathToImg;

        #region ViewModels

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

        #endregion

        #endregion

        #region Methods
        
        private void CloseForm()
        {
            SdjMainViewModel.GetImageVisibility = GetImageVisibility.Collapsed;
        }

        #endregion

        #region Commands

        #region AddImageByLocalCommand

        private RelayCommand _addImageByLocalCommand;

        public RelayCommand AddImageByLocalCommand
        {
            get
            {
                return _addImageByLocalCommand
                       ?? (_addImageByLocalCommand = new RelayCommand(AddImageByLocalCommandExecute,
                           AddImageByLocalCommandCanExecute));
            }
        }

        public bool AddImageByLocalCommandCanExecute()
        {
            return true;
        }

        public void AddImageByLocalCommandExecute()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg,*.jpg,*.JPG,*|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                _localPathToImg = openFileDialog.FileName;
        }

        #endregion

        #region AddImageByUrlCommand

        private RelayCommand _addImageByUrlCommand;

        public RelayCommand AddImageByUrlCommand
        {
            get
            {
                return _addImageByUrlCommand
                       ?? (_addImageByUrlCommand =
                           new RelayCommand(AddImageByUrlCommandExecute, AddImageByUrlCommandCanExecute));
            }
        }

        public bool AddImageByUrlCommandCanExecute()
        {
            return true;
        }

        public void AddImageByUrlCommandExecute()
        {
            SdjMainViewModel.GetImageVisibility = GetImageVisibility.GetByLink;
        }

        #endregion
        
        #region AcceptChangesCommand
        private RelayCommand _acceptChangesCommand;
        public RelayCommand AcceptChangesCommand
        {
            get
            {
                return _acceptChangesCommand
                       ?? (_acceptChangesCommand = new RelayCommand(AcceptChangesCommandExecute, AcceptChangesCommandCanExecute));
            }
        }

        public bool AcceptChangesCommandCanExecute()
        {
            return true;
        }

        public void AcceptChangesCommandExecute()
        {
            
        }
        #endregion

        #endregion
    }
}