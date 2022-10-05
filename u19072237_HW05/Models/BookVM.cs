using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u19072237_HW05.Models;

namespace u19072237_HW05.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }

        public Author Author { get; set; }

        public Typ Type { get; set; }

        public string Status { get; set; }
       

    }
}