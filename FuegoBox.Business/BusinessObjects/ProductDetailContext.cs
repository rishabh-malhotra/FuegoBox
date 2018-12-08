using AutoMapper;
using FuegoBox.DAL.DBObjects;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class ProductDetailContext
    {
        ProductDetailDB ProductDBObject;

        public ProductDetailContext()
        {
            ProductDBObject = new ProductDetailDB();
        }
        public ProductDetailDTO GetProductDetail(ProductDetailDTO productDetailDTO)
        {
            ProductDetailDTO produDetailDTO = ProductDBObject.GetDetail(productDetailDTO);
            return produDetailDTO;
        }

        public CardDTO productAddToCart(ProductDetailDTO productDetailDTO)
        {
            CardDTO cDTO = ProductDBObject.AddProduct(productDetailDTO);
            return cDTO;
        }
        public ProductSearchResultDTO GetProductwithString(string searchString)
        {
            ProductSearchResultDTO produDetailDTO = ProductDBObject.GetProductSearch(searchString);
            return produDetailDTO;
        }

    }
}