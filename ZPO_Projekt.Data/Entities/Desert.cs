using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class Desert : Food
    {
        [DisplayName("Contains milk?")]
        public bool ContainsMilk { get; set; }
    }
}