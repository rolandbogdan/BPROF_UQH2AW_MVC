﻿using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        public IActionResult ListProducts()
        {
            return View(productlogic.GetAllProducts());
        }
        public IActionResult ProductDataGenerator()
        {
            StreamWriter sw = new StreamWriter("Saves/products.json");
            foreach (var item in productlogic.GetAllProducts())
            {
                sw.Write(JsonConvert.SerializeObject(item));
            }
            sw.Close();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GenerateData()
        {
            StreamReader sr = new StreamReader("Saves/products.json");
            string pr = sr.ReadToEnd();
            this.productlogic.AddProduct(JsonConvert.DeserializeObject<Product>(pr));
            return RedirectToAction(nameof(Index));
        }
        public IActionResult LoginPage()
        {
            return View();
        }
        public IActionResult RegisterPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserIndex()
        {
            //if felhasználónév+jelszó nem jó, return rossz jelszó, else
            return View();
        }
        public IActionResult LogOut()
        {
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AdminIndex()
        {
            //if felhasználónév+jelszó nem jó, return rossz jelszó, else
            return View();
        }
    }
}
