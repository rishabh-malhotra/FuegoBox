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

        IMapper AddressMapper;
        public OrderController()
        {
            orderBusinessContext = new OrderBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddressModel, AddressDTO>();
            });
            AddressMapper = new Mapper(config);
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
                    orderBusinessContext.PlaceOrder(new Guid(Session["UserID"].ToString()), addressDTO);
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
    }
}