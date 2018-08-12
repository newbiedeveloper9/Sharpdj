using System;
using System.Collections.Generic;
using Communication.Shared;

namespace Communication.Client.Logic
{
    public class ClientReceiverEvents
    {
        #region Events

        public event EventHandler<UserDisconnectEventArgs> UserDisconnect;
        

        protected virtual void OnUserDisconnect(UserDisconnectEventArgs e)
        {
            var handler = UserDisconnect;
            handler?.Invoke(this, e);
        }

        #region Succesful
        public event EventHandler SuccesfulRegister;
        public event EventHandler<SuccesfulLoginEventArgs> SuccessfulLogin;
        public event EventHandler<GetPeopleEventArgs> GetPeople;

        protected virtual void OnSuccessfulLogin(SuccesfulLoginEventArgs e)
        {
            var handler = SuccessfulLogin;
            handler?.Invoke(this, e);
        }
        protected virtual void OnSuccesfulRegister(EventArgs e)
        {
            var handler = SuccesfulRegister;
            handler?.Invoke(this, e);
        }
        protected virtual void OnGetPeople(GetPeopleEventArgs e)
        {
            var handler = GetPeople;
            handler?.Invoke(this, e);
        }

        public class GetPeopleEventArgs : System.EventArgs
        {
            public GetPeopleEventArgs(List<UserClient> userClients)
            {
                this.UserClients = userClients;
            }

            public List<UserClient> UserClients { get; private set; }
        }

        public class SuccesfulLoginEventArgs : System.EventArgs
        {
            public SuccesfulLoginEventArgs(string username, Rank rank)
            {
                Username = username;
                Rank = rank;
            }

            public string Username { get; private set; }
            public Rank Rank { get; private set; }

        }
        #endregion Succesful
        #region Errors
        public event EventHandler LoginErr;
        public event EventHandler RegisterErr;
        public event EventHandler RegisterAccExistErr;
        public event EventHandler GetPeopleErr;


        protected virtual void OnLoginErr(EventArgs e)
        {
            EventHandler eh = LoginErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnRegisterErr(EventArgs e)
        {
            EventHandler eh = RegisterErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnRegisterAccExistErr(EventArgs e)
        {
            EventHandler eh = RegisterAccExistErr;
            eh?.Invoke(this, e);
        }
        protected virtual void OnGetPeopleErr(EventArgs e)
        {
            EventHandler eh = GetPeopleErr;
            eh?.Invoke(this, e);
        }
        #endregion
        #region UserAccount Errors

        public event EventHandler ChangePasswordError;
        public event EventHandler ChangeUsernameError;
        public event EventHandler ChangeLoginError;
        public event EventHandler ChangeRankError;


        protected virtual void OnChangePasswordError(EventArgs e)
        {
            EventHandler eh = ChangePasswordError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeUsernameError(EventArgs e)
        {
            EventHandler eh = ChangeUsernameError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeLoginError(EventArgs e)
        {
            EventHandler eh = ChangeLoginError;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeRankError(EventArgs e)
        {
            EventHandler eh = ChangeRankError;
            eh?.Invoke(this, e);
        }
        #endregion
        #region UserAccount Successful
        public event EventHandler ChangePasswordSuccessful;
        public event EventHandler ChangeUsernameSuccessful;
        public event EventHandler ChangeLoginSuccessful;
        public event EventHandler ChangeRankSuccessful;


        protected virtual void OnChangePasswordSuccessful(EventArgs e)
        {
            EventHandler eh = ChangePasswordSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeUsernameSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeUsernameSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeLoginSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeLoginSuccessful;
            eh?.Invoke(this, e);
        }
        protected virtual void OnChangeRankSuccessful(EventArgs e)
        {
            EventHandler eh = ChangeRankSuccessful;
            eh?.Invoke(this, e);
        }
        #endregion



        public class UserDisconnectEventArgs : System.EventArgs
        {
            public UserDisconnectEventArgs(string username)
            {
                this.Username = username;
            }

            public string Username { get; private set; }
        }
        #endregion
    }
}