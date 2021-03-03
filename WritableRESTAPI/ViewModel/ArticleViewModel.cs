using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.ViewModel
{
    public class ArticleViewModel
    {
        public string Abstract { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        public UserViewModel Author { get; set; }
    }
}
