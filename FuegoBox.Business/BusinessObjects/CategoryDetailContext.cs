using FuegoBox.DAL.DBObjects;
using FuegoBox.Shared.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class CategoryDetailContext
    {
        CategoryProductDB catDBObject;

        public CategoryDetailContext()
        {
            catDBObject = new CategoryProductDB();
        }
        public CategoryDTO GetCategoryProduct(string catName)
        {
            CategoryDTO catproductDTO = catDBObject.Getproduct(catName);
            return catproductDTO;
        }
        public CategoryDTO GetCategoryOnHomePage()
        {
            CategoryDTO cdto = catDBObject.GetCategoryonHomePage();
            return cdto;
        }
    }
}