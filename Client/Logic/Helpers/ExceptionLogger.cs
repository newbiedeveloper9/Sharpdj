using System;
using Communication.Shared;

namespace SharpDj.Logic.Helpers
{
    class ExceptionLogger
    {
        public ExceptionLogger(Exception e)
        {
            Debug.Log($"Exception:{e.Source}", $"{e.Message}" );
            Debug.Log($"StackTrace", $"{e.StackTrace}" );
        }
    }
}
