using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public class VariantDTO
    {
        public double ListingPrice { get; set; }
        public double Discount { get; set; }
        public int QuantitySold { get; set; }
        public int Inventory { get; set; }
        public ProductDetailDTO Product;
        public string Variant_Property { get; set; }
        public string Variant_Value1 { get; set; }
        public string Variant_Value2 { get; set; }
        public string image { get; set; }

    }
}
