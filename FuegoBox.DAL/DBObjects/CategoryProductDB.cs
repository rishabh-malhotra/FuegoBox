using AutoMapper;
using FuegoBox.DAL.Exceptions;
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
        IMapper P_DTOmapper;
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
            categoryDTO.Name = catName;
            categoryDTO.Products = (from pi in dbContext.Product.Where(c => c.CategoryID == idvalue)
                                    join v in dbContext.Variant on pi.ID equals v.ProductID
                                    join img in dbContext.VariantImage on v.ID equals img.VariantID
                                    select new ProductDetailDTO()
                                    {
                                        ImageURL = img.ImageURL,
                                        Name = pi.Name,
                                        Discount = v.Discount,
                                        ListingPrice = v.ListingPrice

                                    }).ToList();

            //  categoryDTO.Products = P_DTOmapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailDTO>>(product1);
            return categoryDTO;
        }
        public CategoryDTO GetCategoryonHomePage()
        {


            CategoryDTO cd = new CategoryDTO();
            //IEnumerable<Product> pr = new List<Product>();
            List<List<ProductDetailDTO>> abcd = new List<List<ProductDetailDTO>>();
            var categories = dbContext.Category.Include(abc => abc.Product).OrderByDescending(cdd => cdd.ProductsSold).ToList().Take(3);
            foreach (Category cato in categories)
            {
                cd.Products = (from pi in dbContext.Product
                               where pi.CategoryID == cato.ID
                               join v in dbContext.Variant on pi.ID equals v.ProductID
                               orderby v.QuantitySold descending
                               select new ProductDetailDTO()
                               {
                                   Name = pi.Name,
                                   CatName = cato.Name,
                                   ListingPrice = v.ListingPrice,
                                   Discount = v.Discount,

                               }).ToList().Take(3);


                abcd.Add(cd.Products.ToList());

            }
            List<ProductDetailDTO> prs = new List<ProductDetailDTO>();
            foreach (var asd in abcd)
            {
                foreach (var ass in asd)
                {
                    prs.Add(ass);
                }
            }
            cd.Products = prs;
            return cd;
        }
        public bool CategoryExists(string CategoryName)
        {
            Category category = dbContext.Category.Where(c => c.Name == CategoryName).FirstOrDefault();
            if (category == null)
            {
                throw new NotFoundException();
            }
            return true;
        }
    }
}