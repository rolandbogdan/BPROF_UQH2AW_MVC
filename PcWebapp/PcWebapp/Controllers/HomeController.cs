using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Models;
using Newtonsoft.Json;
using System.IO;
using Repository;

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
        public IActionResult EditProduct()
        {
            return View();
        }
        public IActionResult DeleteProduct()
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
        public IActionResult EditOrder()
        {
            return View();
        }
        #endregion
        public IActionResult GenerateData() //json-ból beolvasás
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
