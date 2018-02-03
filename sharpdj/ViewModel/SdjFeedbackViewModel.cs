using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.ViewModel
{
   public class SdjFeedbackViewModel : BaseViewModel
    {
        #region .ctor
        public SdjFeedbackViewModel(SdjMainViewModel main)
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

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value) return;
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }



        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands


        #endregion Commands


    }
}
