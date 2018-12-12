using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CartProductModel
    {
        public string Name { get; set; }
        public double SellingPrice { get; set; }
        public System.Guid ID { get; set; }
        public System.Guid Variant_ID { get; set; }
        public string Url { get; set; }
        public double Result { get; set; }
    }
}