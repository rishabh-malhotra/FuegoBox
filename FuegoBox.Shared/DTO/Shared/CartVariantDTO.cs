using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Shared
{
    public class CartVariantDTO
    {
        public string Name;
        public VariantDTO Variant;
        public Guid ID;
        public int Qty;
        
    }
}
