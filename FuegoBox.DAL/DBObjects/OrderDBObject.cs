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

    }
}
