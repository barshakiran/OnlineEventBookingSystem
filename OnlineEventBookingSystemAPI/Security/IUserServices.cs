using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemAPI.Security
{
    interface IUserServices
    {
        bool Authenticate(string userName, string word);
    }
}
