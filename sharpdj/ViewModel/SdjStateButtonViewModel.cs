using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDj.Enums;

namespace SharpDj.ViewModel
{
    public class SdjStateButtonViewModel : BaseViewModel
    {
        private readonly int _indexWindow;

        #region .ctor

        public SdjStateButtonViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;

            for (int i = 0; i < App.Current.Windows.Count; i++)
                if (App.Current.Windows[i].Title.Equals("Main"))
                    _indexWindow = i;
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

        void CloseApp()
        {
            Console.WriteLine("close");
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                Console.WriteLine("Canceled exit");
            }
            else
            {
                App.Current.Windows[_indexWindow].Close();
            }
        }


        #endregion Methods

        #region Commands


        #region LeftButtonDownOnTitleBarCommand
        private RelayCommand _leftButtonDownOnTitleBarCommand;
        public RelayCommand LeftButtonDownOnTitleBarCommand
        {
            get
            {
                return _leftButtonDownOnTitleBarCommand
                       ?? (_leftButtonDownOnTitleBarCommand = new RelayCommand(LeftButtonDownOnTitleBarCommandExecute, LeftButtonDownOnTitleBarCommandCanExecute));
            }
        }

        public bool LeftButtonDownOnTitleBarCommandCanExecute()
        {
            return true;
        }

        public void LeftButtonDownOnTitleBarCommandExecute()
        {
            Console.WriteLine("dragmove");
            App.Current.Windows[_indexWindow].DragMove();
        }
        #endregion

        #region StateButtonCloseCommand
        private RelayCommand _stateButtonCloseCommand;
        public RelayCommand StateButtonCloseCommand
        {
            get
            {
                return _stateButtonCloseCommand
                       ?? (_stateButtonCloseCommand = new RelayCommand(StateButtonCloseCommandExecute, StateButtonCloseCommandCanExecute));
            }
        }

        public bool StateButtonCloseCommandCanExecute()
        {
            return true;
        }

        public void StateButtonCloseCommandExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
            else if (SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility == Playlist.Visible)
                SdjMainViewModel.SdjPlaylistViewModel.PlaylistVisibility = Playlist.Collapsed;
            
            else if (SdjMainViewModel.MainViewVisibility != MainView.Main)
                SdjMainViewModel.MainViewVisibility = MainView.Main;
            else
                CloseApp();
        }
        #endregion

        #region StateButtonMaximizeCommand
        private RelayCommand _stateButtonMaximizeCommand;
        public RelayCommand StateButtonMaximizeCommand
        {
            get
            {
                return _stateButtonMaximizeCommand
                       ?? (_stateButtonMaximizeCommand = new RelayCommand(StateButtonMaximizeCommandExecute, StateButtonMaximizeCommandCanExecute));
            }
        }

        public bool StateButtonMaximizeCommandCanExecute()
        {
            return true;
        }

        private double normalHeight = 0;
        private double normalWidth = 0;
        private double normalX = 0;
        private double normalY = 0;
        public void StateButtonMaximizeCommandExecute()
        {
            if (App.Current.Windows[_indexWindow].Height == SystemParameters.WorkArea.Height && App.Current.Windows[_indexWindow].Width == SystemParameters.WorkArea.Width)
            {
                App.Current.Windows[_indexWindow].WindowState = WindowState.Normal;
                App.Current.Windows[_indexWindow].Height = normalHeight;
                App.Current.Windows[_indexWindow].Width = normalWidth;
                App.Current.Windows[_indexWindow].Left = normalX;
                App.Current.Windows[_indexWindow].Top = normalY;
            }
            else
            {
                App.Current.Windows[_indexWindow].WindowState = WindowState.Normal;
                normalHeight = App.Current.Windows[_indexWindow].Height;
                normalWidth = App.Current.Windows[_indexWindow].Width;
                normalX = App.Current.Windows[_indexWindow].Left;
                normalY = App.Current.Windows[_indexWindow].Top;

                App.Current.Windows[_indexWindow].Height = SystemParameters.WorkArea.Height;
                App.Current.Windows[_indexWindow].Width = SystemParameters.WorkArea.Width;
                App.Current.Windows[_indexWindow].Left = 0;
                App.Current.Windows[_indexWindow].Top = 0;
            }

            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
        }
        #endregion

        #region StateButtonMinimalizeCommand
        private RelayCommand _stateButtonMinimalizeCommand;
        public RelayCommand StateButtonMinimalizeCommand
        {
            get
            {
                return _stateButtonMinimalizeCommand
                       ?? (_stateButtonMinimalizeCommand = new RelayCommand(StateButtonMinimalizeCommandExecute, StateButtonMinimalizeCommandCanExecute));
            }
        }

        public bool StateButtonMinimalizeCommandCanExecute()
        {
            return true;
        }

        public void StateButtonMinimalizeCommandExecute()
        {
            App.Current.Windows[_indexWindow].WindowState = WindowState.Minimized;

            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
        }
        #endregion


        #endregion Commands
    }
}
