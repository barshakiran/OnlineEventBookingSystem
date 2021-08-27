using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;
using OnlineEventBookingSystemDAL.Infrastructure;
using System.Linq.Expressions;

namespace OnlineEventBookingSystemDAL
{
    public class UserDataHandler : BaseRepository<UserDetail>, IUserDataHandler
    {
        public UserDataHandler(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
