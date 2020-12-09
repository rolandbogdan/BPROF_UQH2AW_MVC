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
            return RedirectToAction(nameof(AdminIndex));
        }
        public IActionResult ListProducts()
        {
            return View(productlogic.GetAllProducts());
        }
        public IActionResult ProductJsonWrite() //json-ba kiírás
        {
            StreamWriter sw = new StreamWriter("Saves/products.json", true);
            foreach (var item in productlogic.GetAllProducts())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            sw.Close();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditProduct()
        {
            return View();
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
            return RedirectToAction(nameof(AdminIndex));
        }
        public IActionResult ListCustomers()
        {
            return View(customerlogic.GetAllCustomers());
        }
        public IActionResult CustomerJsonWrite() //json-ba kiírás
        {
            StreamWriter sw = new StreamWriter("Saves/customers.json",true);
            foreach (var item in customerlogic.GetAllCustomers())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            sw.Close();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditCustomer()
        {
            return View();
        }
        #endregion

        #region Orders
        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOrder(Order o, string cid, string pid)
        {
            o.OrderID = Guid.NewGuid().ToString();
            o.Customers.Add(customerlogic.GetCustomer(cid));
            customerlogic.GetCustomer(cid).Products.Add(productlogic.GetProduct(pid));
            o.OrderDate = DateTime.Now;
            orderlogic.AddOrder(o);
            return RedirectToAction(nameof(AdminIndex));
        }
        public IActionResult ListOrders()
        {
            return View(orderlogic.GetAllOrders());
        }
        public IActionResult OrderJsonWrite() //json-ba kiírás
        {
            StreamWriter sw = new StreamWriter("Saves/orders.json", true);
            foreach (var item in orderlogic.GetAllOrders())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            sw.Close();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditOrder()
        {
            return View();
        }
        #endregion
        public IActionResult JsonRead() //json-ból beolvasás
        {
            StreamReader sr = new StreamReader("Saves/products.json");
            string pr = sr.ReadToEnd();
            this.productlogic.AddProduct(JsonConvert.DeserializeObject<Product>(pr));
            sr.Close();

            sr = new StreamReader("Saves/customers.json");
            string cmr = sr.ReadToEnd();
            this.customerlogic.AddCustomer(JsonConvert.DeserializeObject<Customer>(cmr));
            sr.Close();

            sr = new StreamReader("Saves/orders.json");
            string ord = sr.ReadToEnd();
            this.orderlogic.AddOrder(JsonConvert.DeserializeObject<Order>(ord));
            sr.Close();

            return RedirectToAction(nameof(Index));
        }
    }
}
