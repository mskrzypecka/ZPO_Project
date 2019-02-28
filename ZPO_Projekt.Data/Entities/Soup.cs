using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class Soup : Food
    {
        [DisplayName("Is oriental?")]
        public bool IsOriental { get; set; }
    }
}