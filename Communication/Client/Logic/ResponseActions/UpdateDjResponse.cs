using System;
using System.Text.RegularExpressions;

namespace Communication.Client.Logic.ResponseActions
{
    public sealed class UpdateDjResponse : IResponseActions
    {
        public void OnSuccess(GroupCollection groups)
        {
            var json = groups[1].Value;

            OnUpdateDj(new UpdateDjEventArgs(json));
        }

        public void OnFailed(GroupCollection groups)
        {
            
        }
        
        
        public static event EventHandler<UpdateDjEventArgs> UpdateDj;

        private void OnUpdateDj(UpdateDjEventArgs e)
        {
            var handler = UpdateDj;
            handler?.Invoke(this, e);
        }
        
        public class UpdateDjEventArgs : System.EventArgs
        {
            public UpdateDjEventArgs(string json)
            {
                this.Json = json;
            }

            public string Json { get; private set; }
        }
    }
}