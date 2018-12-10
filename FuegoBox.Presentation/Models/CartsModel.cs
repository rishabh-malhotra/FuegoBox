using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CartsModel
    {
        public double SubTotal { get; set; }
        public bool IsLoggedIn { get; set; }
        public IEnumerable<CartVariantModel> CartItems;
    }
}