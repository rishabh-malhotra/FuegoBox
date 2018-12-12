using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FuegoBox.Presentation.Models
{
    public class OrderItemsModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public System.DateTime OrderDate { get; set; }
    }
}