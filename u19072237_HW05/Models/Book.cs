using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19072237_HW05.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Name { get; set; }

        public int PageCount  { get; set; }

        public int Point { get; set; }

        public int AutherID { get; set; }

        public int TypeID { get; set; }

    }
}