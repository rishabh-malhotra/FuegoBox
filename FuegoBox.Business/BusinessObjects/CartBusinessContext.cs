using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.DBObjects;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class CartBusinessContext
    {
        CartDatabaseContext cartDatabaseContext;
        public void AddToCart(CartDTO cartDTO)
        {
            try
            {
                bool alreadyPresent = cartDatabaseContext.PresentInCart(cartDTO);
            }
            catch (AlreadyPresentException ex)
            {
                throw new ItemAlreadyInCartException();
            }
            cartDatabaseContext.AddToCart(cartDTO);
        }
    }
}
