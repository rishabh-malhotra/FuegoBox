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
            IEnumerable<Product> product = dbContext.Product.Where(a => a.CategoryID == idvalue).Include(pa => pa.Variant);

            //for (var i = 0; i < product.Count(); i++)
            //{

            //  p.ImageURL = product.ElementAt(i).Variant.ElementAt(i).VariantImage.ElementAt(i).ImageURL;



            //}
            //IEnumerable<Product> product1 = dbContext.Product.Include(i => i.Variant.Select(s => s.VariantImage).Select(a=>a.ImageURL));
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Products = P_DTOmapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailDTO>>(product);
            categoryDTO.Name = "Books";
            return categoryDTO;
        }
    }
}