using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public class CartDTO
    {
        public System.Guid ProductID;
        public System.Guid UserID { get; set; }
        public System.Guid VariantID { get; set; }
        public double SellingPrice { get; set; }
        public int Qty { get; set; }
        public virtual VariantDTO Variant { get; set; }
        public string ImageURL { get; set; }
        public string Variant_Property { get; set; }
        public string Variant_Value1 { get; set; }
        public string Variant_Value2 { get; set; }
        public IEnumerable<ProductDetailDTO> Abc { get; set; }



    }
}
