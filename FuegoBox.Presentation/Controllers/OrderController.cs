using FuegoBox.Presentation.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuegoBox.Shared.DTO.Order;
using FuegoBox.Presentation.Models;
using AutoMapper;
using FuegoBox.Business.BusinessObjects;

namespace FuegoBox.Presentation.Controllers
{


    [UserAuthenticationFilter]
    public class OrderController : Controller
    {
        OrderBusinessContext orderBusinessContext;

        IMapper AddressMapper, omapper, OrderMapper;
        public OrderController()
        {
            orderBusinessContext = new OrderBusinessContext();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressModel, AddressDTO>();
            });
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ViewOrderDTO, ViewOrderModel>();
            });
            OrderMapper = new Mapper(config);
            omapper = new Mapper(conf);
        }
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout([Bind(Include = ("AddressLine1, AddressLine2, PIN"))] AddressModel addressViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddressDTO addressDTO = AddressMapper.Map<AddressDTO>(addressViewModel);
                    orderBusinessContext.PlaceOrder(addressDTO, new Guid(Session["UserID"].ToString()));
                    return View("Success");
                }
                catch (Exception ex)
                {
                    return View("Internal Error");
                }
            }
            else
            {
                return View(addressViewModel);
            }
        }

        public ActionResult ViewOrderItem()
        {
            ViewOrderModel vom = new ViewOrderModel();
            ViewOrderDTO vodto = new ViewOrderDTO();
            Guid userId = new Guid(Session["UserID"].ToString());
            vodto = orderBusinessContext.viewOrder(userId);
            vom = omapper.Map<ViewOrderDTO, ViewOrderModel>(vodto);
            return View(vom);
        }
    }
}