using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuanCinema.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        TuanCinemaContext dbContext;

        public TuanCinemaContext Init()
        {
            return dbContext ?? (dbContext = new TuanCinemaContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
