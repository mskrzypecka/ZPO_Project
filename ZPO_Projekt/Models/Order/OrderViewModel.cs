using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZPO_Projekt.Data.Entities;

namespace ZPO_Projekt.Models
{
    public class OrderViewModel
    {
        public DeliveryType Delivery { get; set; }
        public List<Food> Foods { get; set; }
    }
}