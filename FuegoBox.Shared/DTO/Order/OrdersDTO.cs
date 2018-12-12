using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Order
{
    public class OrdersDTO
    {
        public IEnumerable<OrderDTO> orders { get; set; }
    }
}
