using OnlineEventBookingSystemDomain;
using System.Collections.Generic;

namespace OnlineEventBookingSystemBL.Interface
{
   public interface IEventDetailBusiness
    {
        //Event CRUD operations
        string AddEventDetails(EventDetailDomainModel eventDModel);
       // List<EventDetailDomainModel> DisplayEventDetails();
       // EventDetailDomainModel DisplayEvent(int eventId);
        string UpdateEventDetails(EventDetailDomainModel eventDModel);
        bool DeleteEvent(int id, int locationId);
        List<LocationDomainModel> LocationDetailList();
        List<EventDetailDomainModel> DisplayEventDetailList();
        EventDetailDomainModel DisplayEventDetail(int eventId, int locationId);
    }
}
