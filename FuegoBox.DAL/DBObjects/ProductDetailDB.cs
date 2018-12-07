using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
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
                cfg.CreateMap<Product,SearchDTO>();
                cfg.CreateMap<Variant, VariantDTO>();
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
                newBasicDTO.img = vi.ImageURL;
                return newBasicDTO;
            }
            return null;
        }
        public CardDTO AddProduct(ProductDetailDTO pdto)
        {
            Product product = dbContext.Product.Where(a => a.Name == pdto.Name).FirstOrDefault();
            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            // ProductDetailDTO newBasicDTO = P_DTOmapper.Map<Product, ProductDetailDTO>(product);
            CardDTO cartdto = new CardDTO();
            Cart cart = new Cart();
            // Cart cart = cart_mapper.Map<CardDTO, Cart>(cartdto);
            cart.ID = Guid.NewGuid();
            cart.VariantID = variant.ID;
            cart.SellingPrice = variant.Discount;
            cart.Qty = 2;
            cartdto.VariantID = variant.ID;
            cartdto.SellingPrice = variant.Discount;

            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
            return cartdto;



        }

        public SearchResultsDTO GetProductsWithString(string SearchString)
        {
            IEnumerable<Product> searchResults = dbContext.Product.Where(p => p.Name.Contains(SearchString));
            SearchResultsDTO newProductsSearchResultDTO = new SearchResultsDTO();
            newProductsSearchResultDTO.Products = SearchMapper.Map<IEnumerable<Product>, IEnumerable<SearchDTO>>(searchResults);
            return newProductsSearchResultDTO;
        }

        //public List<Cart> GetCardItems()
        //{

        //  return dbContext.Cart.ToList();


        //}


    }
}