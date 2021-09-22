using OnlineEventBookingSystemDomain;
using System.Collections.Generic;

namespace OnlineEventBookingSystemBL.Interface
{
   public interface IEventDetailBusiness
    {
        string AddEventDetails(EventDetailDomainModel eventDModel);
        string UpdateEventDetails(EventDetailDomainModel eventDModel);
        bool DeleteEvent(int id, int locationId);
        List<LocationDomainModel> LocationDetailList();
        List<EventDetailDomainModel> DisplayEventDetailList();
        EventDetailDomainModel DisplayEventDetail(int eventId, int locationId);
        List<BookingDetailDomainModel> DisplayBookedEventsList();
    }
}
