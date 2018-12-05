using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class ProductModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid CategoryID { get; set; }
        public string Description { get; set; }
        public int OrderLimit { get; set; }
    }
}
