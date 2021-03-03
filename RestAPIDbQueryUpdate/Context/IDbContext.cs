using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Context
{
    public interface IDbContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
