using System.Text.RegularExpressions;

namespace Communication.Client.Logic.ResponseActions
{
    public interface IResponseActions
    {
        void OnSuccess(GroupCollection groups);
        void OnFailed(GroupCollection groups);
    }
}