using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class MainDish : Food
    {
        [DisplayName("Is spicy?")]
        public bool IsSpicy { get; set; }
    }
}