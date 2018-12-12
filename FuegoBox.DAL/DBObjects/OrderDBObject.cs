using AutoMapper;
using FuegoBox.Shared.DTO.Order;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class OrderDBObject
    {
        FuegoEntities dbContext;
        IMapper AddressMapper;
        public OrderDBObject()
        {
            dbContext = new FuegoEntities();
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressDTO, Address>();
            });
            ;
            AddressMapper = new Mapper(conf);
        }

        public Guid AddAddress(AddressDTO addDTO, Guid userid)
        {
            Address address = AddressMapper.Map<AddressDTO, Address>(addDTO);
            address.UserID = userid;
            address.ID = Guid.NewGuid();
            dbContext.Address.Add(address);
            dbContext.SaveChanges();
            return address.ID;
        }

        public void PlaceOrder(Guid userid, CartsDTO cdto, Guid addressid)
        {
            Order order = new Order();
            order.ID = Guid.NewGuid();
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = DateTime.Now.AddDays(2);
            order.UserID = userid;
            order.DeliveryAddressID = addressid;
            order.isCancelled = "N";
            order.TotalAmount = cdto.SubTotal;
            dbContext.Order.Add(order);
            dbContext.SaveChanges();
        }

        public ViewOrderDTO ViewOrder(Guid userid)
        {
            ViewOrderDTO viewcdto = new ViewOrderDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            viewcdto.OrderItems = (from or in dbContext.Order.Where(cdd => cdd.UserID == userid)
                                   join cart in dbContext.Cart on or.UserID equals cart.UserID
                                   join vari in dbContext.Variant on cart.VariantID equals vari.ID
                                   join img in dbContext.VariantImage on vari.ID equals img.VariantID
                                   join p in dbContext.Product on vari.ProductID equals p.ID

                                   select new OrderItemsDTO()
                                   {
                                       OrderDate = or.OrderDate,
                                       Name = p.Name,
                                       Price = vari.Discount,
                                       Url = img.ImageURL


                                   });
            return viewcdto;
        }

    }
}
