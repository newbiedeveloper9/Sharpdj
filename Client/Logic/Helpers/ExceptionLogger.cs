using System;

namespace SharpDj.Logic.Helpers
{
    class ExceptionLogger
    {
        public ExceptionLogger(Exception e)
        {
            Console.WriteLine($"Exception:{e.Source}", $"{e.Message}" );
            Console.WriteLine($"StackTrace", $"{e.StackTrace}" );
        }
    }
}
