using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Variant
{
    class VariantDTO
    {
        public System.Guid ID { get; set; }
        public System.Guid ProductID { get; set; }
        public int ListingPrice { get; set; }
        public int Discount { get; set; }
        public int QuantitySold { get; set; }
        public int Inventory { get; set; }
    }
}
