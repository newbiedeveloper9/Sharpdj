using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Models;

namespace SharpDj.PubSubModels
{
    public class MinimizeChatPublish : IMinimizeChatPublish
    {
        public ConversationModel Model { get; set; }

        public MinimizeChatPublish(ConversationModel model)
        {
            Model = model;
        }
    }

    public interface IMinimizeChatPublish
    {
        ConversationModel Model { get; set; }
    }
}
