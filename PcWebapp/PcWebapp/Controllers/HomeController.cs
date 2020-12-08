using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Models;
using Newtonsoft.Json;
using System.IO;

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
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            p.ProductID = Guid.NewGuid().ToString();
            productlogic.AddProduct(p);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListProducts()
        {
            return View(productlogic.GetAllProducts());
        }
        public IActionResult ProductDataGenerator()
        {
            StreamWriter sw = new StreamWriter("products.json");
            foreach (var item in productlogic.GetAllProducts())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DataGenerator()
        {
            return View();
        }
    }
}
