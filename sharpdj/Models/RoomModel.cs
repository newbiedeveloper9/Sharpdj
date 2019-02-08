using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public Activity Status { get; set; }
        public int AmountOfPeople { get; set; }
        public int AmountOfAdministration { get; set; }

        public enum Activity
        {
            Active,
            Sleep,
            InActive
        }
    }
}
