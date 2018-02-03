using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SharpDj.Core;
using SharpDj.ViewModel.Helpers;

namespace SharpDj.ViewModel
{
    public class SdjFeedbackViewModel : BaseViewModel
    {
        #region .ctor

        public SdjFeedbackViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;

            SdjBackgroundForFormsViewModel = new SdjBackgroundForFormsViewModel(main, CloseForm);
            SdjTitleBarForUserControlsViewModel = new SdjTitleBarForUserControlsViewModel(main, CloseForm);
            SdjTitleBarForUserControlsViewModel.FormName = "Feedback";
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

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value) return;
                _isVisible = value;
                OnPropertyChanged("IsVisible");
                if (value)
                    FeedbackDate = DateTime.Now;
            }
        }

        private ComboBoxItem _feedbackType;
        public ComboBoxItem FeedbackType
        {
            get => _feedbackType;
            set
            {
                if (_feedbackType == value) return;
                _feedbackType = value;
                OnPropertyChanged("FeedbackType");
            }
        }

        private DateTime _feedbackDate;
        public DateTime FeedbackDate
        {
            get => _feedbackDate;
            set
            {
                if (_feedbackDate == value) return;
                _feedbackDate = value;
                OnPropertyChanged("FeedbackDate");
            }
        }

        private string _feedbackTopic;
        public string FeedbackTopic
        {
            get => _feedbackTopic;
            set
            {
                if (_feedbackTopic == value) return;
                _feedbackTopic = value;
                OnPropertyChanged("FeedbackTopic");
            }
        }

        private string _feedbackText;
        public string FeedbackText
        {
            get => _feedbackText;
            set
            {
                if (_feedbackText == value) return;
                _feedbackText = value;
                OnPropertyChanged("FeedbackText");
            }
        }

        private string _attachmentPath;
        public string AttachmentPath
        {
            get => _attachmentPath;
            set
            {
                if (_attachmentPath == value) return;
                _attachmentPath = value;
                OnPropertyChanged("AttachmentPath");
            }
        }

        private string _errorNotify;
        public string ErrorNotify
        {
            get => _errorNotify;
            set
            {
                if (_errorNotify == value) return;
                _errorNotify = value;
                OnPropertyChanged("ErrorNotify");
            }
        }

        #endregion Properties

        #region Methods

        private void CloseForm()
        {
            IsVisible = false;
        }

        #endregion Methods

        #region Commands

        #region SendFeedbackCommand
        private RelayCommand _sendFeedbackCommand;
        public RelayCommand SendFeedbackCommand
        {
            get
            {
                return _sendFeedbackCommand
                       ?? (_sendFeedbackCommand = new RelayCommand(SendFeedbackCommandExecute, SendFeedbackCommandCanExecute));
            }
        }

        public bool SendFeedbackCommandCanExecute()
        {
            return true;
        }

        public void SendFeedbackCommandExecute()
        {

        }
        #endregion

        #region ChooseAttachmentCommand
        private RelayCommand _chooseAttachmentCommand;
        public RelayCommand ChooseAttachmentCommand
        {
            get
            {
                return _chooseAttachmentCommand
                       ?? (_chooseAttachmentCommand = new RelayCommand(ChooseAttachmentCommandExecute, ChooseAttachmentCommandCanExecute));
            }
        }

        public bool ChooseAttachmentCommandCanExecute()
        {
            return true;
        }

        public void ChooseAttachmentCommandExecute()
        {

        }
        #endregion
        #endregion Commands


    }
}
