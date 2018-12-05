using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FuegoBox.Shared.DTO.Category
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public IEnumerable<ProductDetailDTO> Products { get; set; }
    }
}