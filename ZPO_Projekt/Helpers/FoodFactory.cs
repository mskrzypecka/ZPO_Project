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
    }
}