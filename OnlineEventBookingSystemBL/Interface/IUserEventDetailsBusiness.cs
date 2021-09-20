using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineEventBookingSystemBL.Interface
{
   public interface IUserEventDetailsBusiness
    {
        List<EventDetailDomainModel> DisplayAllUserEvent(string eventType, string city);
        List<LocationDomainModel> DisplayCityList();
    }
}
