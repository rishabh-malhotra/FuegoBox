using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class VariantModel
    {
        public double ListingPrice { get; set; }
        public double Discount { get; set; }
        public ProductDetail Product;
        public string Variant_Property { get; set; }
        public string Variant_Value1 { get; set; }
        public string Variant_Value2 { get; set; }
        public string image { get; set; }
        public int QuantitySold { get; set; }
        public int Inventory { get; set; }
    }
}