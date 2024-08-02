using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.DbContext.Interfaces
{
    public interface IAppDbContext
    {
        IDbConnection GetDbConnection();
    }
}
