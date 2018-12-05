using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public class CardDTO
    {
        public System.Guid VariantID { get; set; }
        public double SellingPrice { get; set; }
        public int Qty { get; set; }
        public virtual VariantDTO Variant { get; set; }
    }
}
