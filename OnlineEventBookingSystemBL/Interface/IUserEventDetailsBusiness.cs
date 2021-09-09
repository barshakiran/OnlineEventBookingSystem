using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineEventBookingSystemBL.Interface
{
   public interface IUserEventDetailsBusiness
    {
        List<UserEventDetailsDomainModel> DisplayAllUserEvent(string eventType, string city);
      //  List<string> DisplayEventTypesList();
        List<EventLocationDomainModel> DisplayCityList();
       // string AddEventDetails(UserEventDetailsDomainModel eventDModel);
    }
}
