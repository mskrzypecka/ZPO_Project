using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPO_Projekt.Data.Entities;

namespace ZPO_Projekt.Data
{
    public interface IFood
    {
        [NotMapped]
        string Id { get; set; }
        [NotMapped]
        string Name { get; set; }
        [NotMapped]
        decimal Price { get; set; }
        [NotMapped]
        DishType Type { get; set; }
        [NotMapped]
        bool IsChecked { get; set; }

        string GetDescription();
    }
}
