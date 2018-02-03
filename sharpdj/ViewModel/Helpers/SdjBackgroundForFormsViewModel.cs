using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Core;
using SharpDj.ViewModel;

namespace SharpDj.ViewModel.Helpers
{
    public class SdjBackgroundForFormsViewModel : BaseViewModel
    {
        #region .ctor
        public SdjBackgroundForFormsViewModel(SdjMainViewModel main, Action closeFormAction)
        {
            SdjMainViewModel = main;
            CloseFormExecute = closeFormAction;
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

        #region CloseForm
        private RelayCommand _closeForm;
        public RelayCommand CloseForm
        {
            get
            {
                return _closeForm
                       ?? (_closeForm = new RelayCommand(CloseFormExecute, CloseFormCanExecute));
            }
        }

        public bool CloseFormCanExecute()
        {
            return true;
        }

        public Action CloseFormExecute { get; set; }
        #endregion


        #endregion Commands
    }
}
