using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CartMessageModel
    {
        public string SuccessMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
        public bool IsLoggedIn { get; set; }
        public CartMessageModel()
        {
            ErrorMessages = new List<string>();
        }
    }
}