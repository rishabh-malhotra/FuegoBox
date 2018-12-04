using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FuegoBox.Shared.DTO.Product
{
    public class ProductDTO
    {

        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public System.Guid CategoryID { get; set; }
        public string Description { get; set; }
        public int OrderLimit { get; set; }



    }
}
