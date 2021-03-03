using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Entity
{
    public class Like : EntityBase
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }

    }
}
