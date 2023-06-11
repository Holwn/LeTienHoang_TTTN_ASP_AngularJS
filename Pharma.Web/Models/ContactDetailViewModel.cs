using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharma.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Website { set; get; }
        public string Address { set; get; }
        public string Other { set; get; }
        public double? Lat { set; get; }
        public double? Lng { set; get; }
        public bool Status { set; get; }
    }
}