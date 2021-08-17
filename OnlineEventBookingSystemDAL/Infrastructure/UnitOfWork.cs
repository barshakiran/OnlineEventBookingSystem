using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineEventBookingSystemDAL.Infrastructure.Contract;

namespace OnlineEventBookingSystemDAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventBookingSystemEntities1 _dbContext;

        public UnitOfWork()
        {
            _dbContext = new EventBookingSystemEntities1();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
        }
    }

}
