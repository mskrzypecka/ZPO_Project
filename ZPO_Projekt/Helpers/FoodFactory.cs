using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZPO_Projekt.Data;
using ZPO_Projekt.Models.Foods;

namespace ZPO_Projekt.Helpers
{
    class FoodFactory
    {
        public static IFood CreateFood(DishType type)
        {
            switch (type)
            {
                case DishType.MainDish:
                    return new MainDish();
                case DishType.Soup:
                    return new Soup();
                case DishType.Desert:
                    return new Desert();
                default:
                    throw new ArgumentException();
            }
        }

        internal static IFood CreateFood(DishType type, Food model)
        {
            IFood food = null;

            switch (type)
            {
                case DishType.MainDish:
                    food = new MainDish();
                    break;
                case DishType.Soup:
                    food = new Soup();
                    break;
                case DishType.Desert:
                    food = new Desert();
                    break;
                default:
                    throw new ArgumentException();
            }

            food.Id = model.Id;
            food.IsChecked = model.IsChecked;
            food.Name = model.Name;
            food.Price = model.Price;
            food.Type = type;

            return food;
        }
    }
}