using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class Address
    {
        [DisplayName("Miasto")]
        public string City { get; set; }
        [DisplayName("Ulica")]
        public string Street { get; set; }
        [DisplayName("Numer domu")]
        public string Number { get; set; }
    }
}