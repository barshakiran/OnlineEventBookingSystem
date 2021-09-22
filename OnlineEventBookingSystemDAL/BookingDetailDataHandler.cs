using OnlineEventBookingSystemDAL.Infrastructure;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemDAL
{
   public class BookingDetailDataHandler: BaseRepository<BookingDetail>, IBookingDetailDataHandler
    {

        public BookingDetailDataHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
