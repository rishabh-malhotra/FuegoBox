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
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Address address = AddressMapper.Map<AddressDTO, Address>(addDTO);
            address.UserID = userid;
            address.ID = Guid.NewGuid();
            dbContext.Address.Add(address);
            dbContext.SaveChanges();
            return address.ID;
        }

        public void PlaceOrder(Guid userid, CartsDTO cdto, Guid addressid)
        {
            CategoryProductDB cdb = new CategoryProductDB();
            Order order = new Order();
            order.ID = Guid.NewGuid();
            cdb.update(cdto);
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = DateTime.Now.AddDays(2);
            order.UserID = userid;
            order.DeliveryAddressID = addressid;
            order.isCancelled = "N";
            order.TotalAmount = cdto.SubTotal;
            dbContext.Order.Add(order);
            dbContext.SaveChanges();
            cdb.addOrderProduct(order, cdto);
        }

        public ViewOrderDTO ViewOrder(Guid orderID)
        {
            ViewOrderDTO viewcdto = new ViewOrderDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            viewcdto.OrderItems = (from or in dbContext.Order.Where(cdd => cdd.ID==orderID)
                                   join op in dbContext.OrderProduct on or.ID equals op.OrderID
                                   join vari in dbContext.Variant on op.VariantID equals vari.ID
                                   join p in dbContext.Product on vari.ProductID equals p.ID
                                   join img in dbContext.VariantImage on vari.ID equals img.VariantID
                                   orderby or.OrderDate descending
                       
                                   select new OrderItemsDTO()
                                   {
                                       Price = vari.Discount,
                                       Url = img.ImageURL,
                                       OrderDate = or.OrderDate,
                                       Name = p.Name
                                   }).ToList();

            return viewcdto;
        }

        public OrdersDTO GetOrders(Guid user_Id)
        {
            OrdersDTO ordersDTO = new OrdersDTO();
            ordersDTO.orders = (from oc in dbContext.Order
                             where oc.UserID == user_Id
                             select new OrderDTO()
                             {
                                TotalAmount=oc.TotalAmount,
                                OrderDate=oc.OrderDate,
                                DeliveryDate=oc.DeliveryDate,
                                ID=oc.ID
                               
                            }).ToList();


            foreach (var i in ordersDTO.orders)
            {
                if (i.DeliveryDate <= DateTime.Now) {
                    i.status = "Delivered";
                }
                else
                {
                    i.status = "Not Delivered";
                }
            }
            
            return ordersDTO;
 
        }

    }
}

