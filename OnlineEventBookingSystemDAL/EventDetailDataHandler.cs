using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using OnlineEventBookingSystemDAL.Infrastructure;

namespace OnlineEventBookingSystemDAL
{
    public class EventDetailDataHandler : BaseRepository<EventDetail>, IEventDetailDataHandler
    {
        public EventDetailDataHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

    }
}
