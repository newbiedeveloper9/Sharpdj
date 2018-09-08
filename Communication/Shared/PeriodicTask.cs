using System;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.Shared
{
    public static class PeriodicTask
    {
        public static void StartNew(int miliseconds, Action action)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    action();
                    Thread.Sleep(miliseconds);
                }
            });   
        }
    }
}