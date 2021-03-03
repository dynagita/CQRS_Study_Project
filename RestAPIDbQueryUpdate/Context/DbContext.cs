using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Context
{
    public class DbContext : IDbContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
