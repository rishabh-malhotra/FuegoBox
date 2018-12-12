using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FuegoBox.Presentation.Models
{
    public class ViewOrderModel
    {
        public IEnumerable<OrderItemsModel> OrderItems { get; set; }
    }
}