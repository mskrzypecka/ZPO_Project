using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class FoodInOrder
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string FoodId { get; set; }
    }
}