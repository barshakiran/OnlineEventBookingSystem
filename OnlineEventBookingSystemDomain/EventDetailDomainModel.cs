using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemDomain
{
    public class EventDetailDomainModel
    {
        public int Event_Id { get; set; }
        public string Event_Name { get; set; }
        public string Event_Type { get; set; }
        public string Event_Description { get; set; }
        public string Event_Picture { get; set; }
    }
}
