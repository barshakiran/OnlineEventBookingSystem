using OnlineEventBookingSystemDomain;


namespace OnlineEventBookingSystemBL.Interface
{
   public interface IEventDetailBusiness
    {
        //Event CRUD operations
        string AddEventDetails(EventDetailDomainModel eventDModel);
    }
}
