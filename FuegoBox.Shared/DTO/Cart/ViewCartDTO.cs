using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Cart
{
   public class ViewCartDTO
    {
        public double Total { get; set; }
        public IEnumerable<CartProductsDTO> CartProduct;
    }
}
