using FuegoBox.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public class CartsDTO
    {
        public double SubTotal { get; set; }
        public IEnumerable<CartVariantDTO> CartItems { get; set; }
        //public IEnumerable<ProductDetailDTO> Products { get; set; }
    }
}
