using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ZPO_Projekt.Models.Foods;

namespace ZPO_Projekt.Data.Entities
{
    public class Desert : Food, IFood
    {
        public override string GetDescription() => "This is a delicious desert.";
    }
}