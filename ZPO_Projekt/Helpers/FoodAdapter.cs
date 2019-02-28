using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Models.Foods;

namespace ZPO_Projekt.Helpers
{
    public static class FoodAdapter
    {
        internal static IFood RestoreIFood(DishType type, Food model)
        {
            IFood food = FoodFactory.CreateFood(type);
            food.Id = model.Id;
            food.IsChecked = model.IsChecked;
            food.Name = model.Name;
            food.Price = model.Price;
            food.Type = type;

            return food;
        }
    }
}