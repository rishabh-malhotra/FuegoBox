using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
 namespace FuegoBox.Shared.DTO.Order
    {
        public class OrderItemsDTO
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public double Price { get; set; }
            public System.DateTime OrderDate { get; set; }
        }
    }
