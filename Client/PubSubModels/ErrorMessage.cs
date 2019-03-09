using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.PubSubModels
{
    public class MessageQueue : IMessageQueue
    {
        public MessageQueue(string viewName, string message)
        {
            ViewName = viewName;
            Message = message;
        }

        public string ViewName { get; set; }
        public string Message { get; set; }
    }

    public interface IMessageQueue
    {
        string ViewName { get; set; }
        string Message { get; set; }
    }
}
