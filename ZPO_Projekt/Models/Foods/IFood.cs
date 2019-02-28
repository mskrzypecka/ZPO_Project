using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPO_Projekt.Data.Entities;

namespace ZPO_Projekt.Models.Foods
{
    public interface IFood
    {
        string Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        DishType Type { get; set; }
        bool IsChecked { get; set; }

        string GetDescription();
    }
}
