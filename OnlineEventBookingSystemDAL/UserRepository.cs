using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using OnlineEventBookingSystemDAL.Infrastructure;

namespace OnlineEventBookingSystemDAL
{
    public class UserRepository : BaseRepository<UserDetail>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        { }
    }
}
