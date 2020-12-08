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
        public IActionResult UserIndex()
        {
            return View();
        }
        public IActionResult AdminIndex()
        {
            return View();
        }

        #region Products
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
            return RedirectToAction(nameof(ListProducts));
        }
        public IActionResult ListProducts()
        {
            return View(productlogic.GetAllProducts());
        }
        public IActionResult ProductDataGenerator() //json-ba kiírás
        {
            StreamWriter sw = new StreamWriter("Saves/products.json");
            foreach (var item in productlogic.GetAllProducts())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            sw.Close();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GenerateData() //json-ból beolvasás
        {
            StreamReader sr = new StreamReader("Saves/products.json");
            string pr = sr.ReadToEnd();
            this.productlogic.AddProduct(JsonConvert.DeserializeObject<Product>(pr));
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Customers
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer c)
        {
            c.CustomerID = Guid.NewGuid().ToString();
            c.RegDate = DateTime.Now;
            customerlogic.AddCustomer(c);
            return RedirectToAction(nameof(ListCustomers));
        }
        public IActionResult ListCustomers()
        {
            return View(customerlogic.GetAllCustomers());
        }
        #endregion

        #region Orders

        #endregion
    }
}
