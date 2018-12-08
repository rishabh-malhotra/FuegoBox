using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Category;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class ProductDetailDB
    {
        FuegoEntities dbContext;
        IMapper ProductSearchMapper;

        IMapper P_DTOmapper, v_DTOmapper, cart_mapper;
        IMapper SearchMapper;
        public ProductDetailDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Variant, VariantDTO>();
            });
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CardDTO, Cart>();
            });
            var productsSearchDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDetailDTO>();

            });


            v_DTOmapper = new Mapper(conf);
            P_DTOmapper = new Mapper(config);
            cart_mapper = new Mapper(configuration);
            SearchMapper = new Mapper(productsSearchDTOConfig);
        }
        public ProductDetailDTO GetDetail(ProductDetailDTO productDetailDTO)
        {
            Product product = dbContext.Product.Where(a => a.Name == productDetailDTO.Name).FirstOrDefault();

            Category cat = dbContext.Category.Where(f => f.ID == product.CategoryID).FirstOrDefault();

            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            VariantImage vi = dbContext.VariantImage.Where(s => s.VariantID == variant.ID).FirstOrDefault();
            if (product != null)
            {
                VariantDTO vdto = v_DTOmapper.Map<Variant, VariantDTO>(variant);
                ProductDetailDTO newBasicDTO = P_DTOmapper.Map<Product, ProductDetailDTO>(product);
                newBasicDTO.ListingPrice = vdto.ListingPrice;
                newBasicDTO.CatName = cat.Name;
                newBasicDTO.Discount = vdto.Discount;
                newBasicDTO.ImageURL = vi.ImageURL;
                newBasicDTO.Inventory =variant.Inventory;
                newBasicDTO.QuantitySold=variant.QuantitySold;
                
                return newBasicDTO;
            }
            return null;
        }
       
        public ProductSearchResultDTO GetProductSearch(string searchString)
        {
            IEnumerable<Product> searchResults = dbContext.Product.Where(p => p.Name.Contains(searchString));
            ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
            // newProductsSearchResultDTO.Products = ProductSearchMapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailDTO>>(searchResults);
            newProductsSearchResultDTO.Products = (from pi in dbContext.Product.Where(p => p.Name.Contains(searchString))
                                                   join v in dbContext.Variant on pi.ID equals v.ProductID
                                                   join img in dbContext.VariantImage on v.ID equals img.VariantID
                                                   select new ProductDetailDTO()
                                                   {
                                                       ImageURL = img.ImageURL,
                                                       Name = pi.Name,
                                                       Description = pi.Description,
                                                       ListingPrice =v.ListingPrice,
                                                       Discount=v.Discount

                                                   }).ToList();
            return newProductsSearchResultDTO;
        }

        //public List<Cart> GetCardItems()
        //{

        //  return dbContext.Cart.ToList();


        //}

        public ProductDetailDTO AddProduct(ProductDetailDTO pdto)
        {
            Product product = dbContext.Product.Where(a => a.Name == pdto.Name).FirstOrDefault();
            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            ProductDetailDTO cartdto = new ProductDetailDTO();
            Cart cart = new Cart();
            cart.ID = Guid.NewGuid();
            cart.VariantID = variant.ID;
            cart.SellingPrice = variant.Discount;
            cart.Qty = 2;
            cartdto.Name = product.Name;
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
            return cartdto;
        }
        }
}