using AutoMapper;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class CategoryProductDB
    {
        FuegoEntities dbContext;
        IMapper P_DTOmapper, c_Mapper;
        public CategoryProductDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });
            P_DTOmapper = new Mapper(config);
        }
        public CategoryDTO Getproduct(string catName)
        {
            System.Guid idvalue;
            Category cat = dbContext.Category.Where(c => c.Name == catName).FirstOrDefault();
            idvalue = cat.ID;
            ProductDetailDTO p = new ProductDetailDTO();
            IEnumerable<Product> product = dbContext.Product.Where(c => c.CategoryID == idvalue);
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Name = "Books";
            categoryDTO.Products = (from pi in dbContext.Product.Where(c => c.CategoryID == idvalue)
                                    join v in dbContext.Variant on pi.ID equals v.ProductID
                                    join img in dbContext.VariantImage on v.ID equals img.VariantID
                                    select new ProductDetailDTO()
                                    {
                                        ImageURL = img.ImageURL,
                                        Name = pi.Name,
                                        Discount=v.Discount,
                                        ListingPrice=v.ListingPrice
   
                                    }).ToList();

            //  categoryDTO.Products = P_DTOmapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailDTO>>(product1);
            return categoryDTO;
        }
    }
}