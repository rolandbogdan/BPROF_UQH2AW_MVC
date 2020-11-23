using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;

namespace PcWebapp.Controllers
{
    public class HomeController : Controller
    {
        CustomerLogic customerlogic;
        ProductLogic productlogic;
        OrderLogic orderlogic;

        public HomeController(CustomerLogic customerlogic, ProductLogic productlogic, OrderLogic orderlogic)
        {
            this.customerlogic = customerlogic;
            this.productlogic = productlogic;
            this.orderlogic = orderlogic;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
