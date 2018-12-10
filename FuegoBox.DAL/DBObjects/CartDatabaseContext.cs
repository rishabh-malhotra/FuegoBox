using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var cartItemsConfig = new MapperConfiguration(cfg =>
            {
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
            Cart cart = new Cart();
            //   Cart cart = new Cart { UserID = cartDTO.UserID, VariantID = cartDTO.VariantID, Qty = cartDTO.Qty };
            Variant vara = dbContext.Variant.Where(va => va.ProductID == cartDTO.ProductID).FirstOrDefault();

            cart.UserID = cartDTO.UserID;
            cart.ID = Guid.NewGuid();
            cart.VariantID = vara.ID;
            cart.Qty = 2;
            cart.SellingPrice = vara.Discount;
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
        }

        public CartsDTO GetCart(Guid UserID)
        {
            var cart = dbContext.Cart.ToList() ;
            CartsDTO cdto = new CartsDTO();
            cdto.CartItems = CartItemsMapper.Map<IEnumerable<Cart>, IEnumerable<CartVariantDTO>>(cart);

            //carts = dbContext.Cart.Include(c=>c.Variant).Where(c => c.UserID == UserID).ToList();
            //carts = (from car in dbContext.Cart.Where(c => c.UserID == UserID)
            //         join vp in dbContext.Variant on car.VariantID equals vp.ID
            //         select new CartDTO()
            //         {


            //         }).ToList();

            //CartsDTO cartsDTO = new CartsDTO();
            //foreach(var ca in carts)
            //{
            //    // cartsDTO.CartItems
            //   cartsDTO.CartItems. =ca.SellingPrice;
            //}
            //cartsDTO.CartItems = CartItemsMapper.Map<IEnumerable<Cart>, IEnumerable<CartVariantDTO>>(carts);

           

            return cdto;

        }
    }
}
