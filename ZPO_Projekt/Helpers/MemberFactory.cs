using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Helpers
{
    class FoodFactory
    {
        public static Food CreateOrder(DishType DishType)
        {
            switch (DishType)
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