﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZPO_Projekt.Data;
using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Models.Foods;

namespace ZPO_Projekt.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<IFood> Foods { get; set; }
        public decimal Price { get => Foods.Sum(x => x.Price); }
    }
}