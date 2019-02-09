using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public NewsModel()
        {
            
        }
    }
}
