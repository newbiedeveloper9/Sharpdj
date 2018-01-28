using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;
using SharpDj.ViewModel;

namespace SharpDj.Models.Client
{
    public class ClientLogic
    {
        public ClientLogic(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
            main.Client.Receiver.SuccessfulLogin += Receiver_SuccessfulLogin;
        }

        private void Receiver_SuccessfulLogin(object sender, EventArgs e)
        {
            SdjMainViewModel.MainViewVisibility = MainView.Main;
        }

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

    }
}
