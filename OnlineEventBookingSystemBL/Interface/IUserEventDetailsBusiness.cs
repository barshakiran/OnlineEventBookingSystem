using OnlineEventBookingSystemDomain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineEventBookingSystemBL.Interface
{
   public interface IUserEventDetailsBusiness
    {

        int AddUserBookingEventDetails(BookingDetailDomainModel bookingDetailDomainModel);
        List<EventDetailDomainModel> DisplayAllUserEvent(string eventType, string city);
        UserEventDetailsDomainModel DisplayUserBookingEventDetails(int eventId,int locationId);
        BookingDetailDomainModel DisplayUserBookedEventDetails(int bookingId);
        List<BookingDetailDomainModel> DisplayUserBookedEventsList(string userName);
        List<LocationDomainModel> DisplayCityList();
        bool DeleteBookedEvent(int id);
       // bool SendEmailNotification(string email, string userName);
    }
}
