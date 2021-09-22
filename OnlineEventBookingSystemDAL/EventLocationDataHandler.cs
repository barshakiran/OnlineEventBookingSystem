using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;

namespace OnlineEventBookingSystemDAL
{
    public class EventLocationDataHandler: BaseRepository<EventLocation>, IEventLocationDataHandler
    {
        public EventLocationDataHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }
    }
}
