using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.DBObjects;
using FuegoBox.DAL.Exceptions;
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

        //display all the product from the particular category...
        public CategoryDTO GetCategoryProduct(string catName)
        {
            try
            {
                bool exists = catDBObject.CategoryExists(catName);
            }
            catch (NotFoundException ex)
            {
                throw new CategoryDoesNotExistsException();
            }
            CategoryDTO catproductDTO = catDBObject.Getproduct(catName);
            return catproductDTO;
        }

        //display product to the home page....
        public CategoryDTO GetCategoryOnHomePage()
        {
            CategoryDTO cdto = catDBObject.GetCategoryonHomePage();
            return cdto;
        }
    }
}