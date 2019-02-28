using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZPO_Projekt.Data.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public DeliveryType Delivery { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of order")]
        public DateTime DateOfOrder { get; set; }
        public ApplicationUser Client { get; set; }
        public List<FoodInOrder> Foods { get; set; }
    }

    public enum DeliveryType
    {
        TakeAway,
        HomeDelivery,
        AtLocation
    }
}