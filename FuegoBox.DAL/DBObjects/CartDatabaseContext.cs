using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class CartDatabaseContext
    {
        FuegoEntities dbContext = new FuegoEntities();
        IMapper CartItemsMapper;
        public CartDatabaseContext()
        {
            var cartItemsConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Variant, VariantDTO>();
                cfg.CreateMap<Cart, CartVariantDTO>();
                cfg.CreateMap<Product, ProductDetailDTO>();
            });
            CartItemsMapper = new Mapper(cartItemsConfig);
        }
        public bool PresentInCart(CartDTO cartDTO)
        {

            IEnumerable<Cart> carts = dbContext.Cart.Where(p => p.UserID == cartDTO.UserID);

            foreach (var cart in carts)
            {
                if (cart.VariantID == cartDTO.VariantID)
                {
                    throw new AlreadyPresentException();
                }
            }
            return false;
        }

        public void AddToCart(CartDTO cartDTO)
        {
            Cart cart = new Cart { UserID = cartDTO.UserID, VariantID = cartDTO.VariantID, Qty = cartDTO.Qty };
            cart.ID=Guid.NewGuid();
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
        }
    }
}
