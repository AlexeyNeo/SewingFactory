using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SewingFactory.Models
{
    public class filtr
    {
            public IEnumerable<PostOrder> Postorders { get; set; }
            public SelectList Order { get; set; }
        
    }
}