using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZPO_Projekt;

namespace ZPO_Projekt.Data.Entities
{
    public class Food
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DishType Type { get; set; }
        public bool IsChecked { get; set; }

        public virtual string GetDescription() => "This is not specified Food but for surte it's delicious.";
    }

    public enum DishType
    {
        MainDish,
        Soup,
        Desert
    }
}