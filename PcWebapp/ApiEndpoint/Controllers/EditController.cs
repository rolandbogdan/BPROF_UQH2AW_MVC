using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class EditController : ControllerBase
    {
        ProductLogic productlogic;
        CustomerLogic customerlogic;
        OrderLogic orderlogic;

        public EditController(CustomerLogic customerlogic, ProductLogic productlogic, OrderLogic orderlogic)
        {
            this.customerlogic = customerlogic;
            this.productlogic = productlogic;
            this.orderlogic = orderlogic;
        }

        [HttpGet]
        public void FillDb()
        {
            #region Orders
            //Példa Béla rendelt egy ryzen 5600-at
            Order o1 = new Order()
            {
                OrderedQuantity = 1,
                OrderDate = new DateTime(2020, 11, 28),
                Comment = "Nem jó a kaputelefon, hívjon mindenképp a futár",
                OrderStatus = OrderStatus.shipped
            };
            orderlogic.AddOrder(o1);

            //Gipsz Jakab rendelt 3 db nztx házat
            Order o2 = new Order()
            {
                OrderedQuantity = 3,
                OrderDate = DateTime.Now,
                Comment = "Nagy címletben fizetek",
                OrderStatus = OrderStatus.pending
            };
            orderlogic.AddOrder(o2);

            //Szuper Szabolcs rendelt 1 db rtx 2080-at
            Order o3 = new Order()
            {
                OrderedQuantity = 1,
                OrderDate = new DateTime(2020, 12, 17),
                Comment = "A leghelyesebb futár jöjjön",
                OrderStatus = OrderStatus.awaiting_shipment
            };
            orderlogic.AddOrder(o3);

            #endregion

            #region Customers
            Customer c1 = new Customer()
            {
                CustomerName = "Példa Béla",
                EmailAddress = "peldabela@email.com",
                PhoneNumber = "+36 12 345 6789",
                Address = "Budapest, Orbán Viktor utca 1.",
                RegDate = new DateTime(2015, 9, 1),
                Password = "Biztonsagosjelszo",
                Order = o1
            };
            customerlogic.AddCustomer(c1);

            Customer c2 = new Customer()
            {
                CustomerName = "Gipsz jakab",
                EmailAddress = "gipszjakab@email.com",
                PhoneNumber = "+36 98 765 4321",
                Address = "Budapest, Mészáros Lőrinc utca 5.",
                RegDate = new DateTime(2020, 12, 2),
                Password = "Asd123",
                Order = o2
            };
            customerlogic.AddCustomer(c2);

            Customer c3 = new Customer()
            {
                CustomerName = "Szuper Szabolcs",
                EmailAddress = "szsz@email.com",
                PhoneNumber = "+36 11 111 1111",
                Address = "Budapest, Szájer József utca 69.",
                RegDate = DateTime.Now,
                Password = "Szuperszabi",
                Order = o3
            };
            customerlogic.AddCustomer(c3);
            #endregion

            #region Products
            Product ryzen5600 = new Product()
            {
                ProductName = "Ryzen 5 5600x",
                Category = ProductCategory.CPU,
                Manufacturer = "AMD",
                Price = 120000,
                InStock = true,
                Quantity = 10,
                Description = "Új amd ryzen processzor",
                CustomerID = c1.CustomerID
            };
            productlogic.AddProduct(ryzen5600);

            Product rtx2080 = new Product()
            {
                ProductName = "RTX 2080",
                Category = ProductCategory.VideoCard,
                Manufacturer = "NVidia",
                Price = 250000,
                InStock = true,
                Quantity = 4,
                Description = "Szuper videókártya",
                CustomerID = c3.CustomerID
            };
            productlogic.AddProduct(rtx2080);

            Product nzxt1 = new Product()
            {
                ProductName = "NZXT 300",
                Category = ProductCategory.Case,
                Manufacturer = "NZXT",
                Price = 15000,
                InStock = true,
                Quantity = 35,
                Description = "NXZT Gépház",
                CustomerID = c2.CustomerID
            };
            productlogic.AddProduct(nzxt1);

            Product rtx3080ti = new Product()
            {
                ProductName = "RTX 3080Ti",
                Category = ProductCategory.VideoCard,
                Manufacturer = "NVidia",
                Price = 400000,
                InStock = false,
                Quantity = 0,
                Description = "Legerősebb videókártya a piacon"
            };
            productlogic.AddProduct(rtx2080);

            Product b550m = new Product()
            {
                ProductName = "B550M DS3H",
                Category = ProductCategory.Motherboard,
                Manufacturer = "Gigabyte",
                Price = 30000,
                InStock = true,
                Quantity = 22,
                Description = "Korrekt alaplap"
            };
            productlogic.AddProduct(b550m);

            Product k16ram = new Product()
            {
                ProductName = "Kingston 2x8 GB Ram",
                Category = ProductCategory.RAM,
                Manufacturer = "Kingston",
                Price = 20000,
                InStock = true,
                Quantity = 18,
                Description = "2x8 GB 3000 MHz ram"
            };
            productlogic.AddProduct(rtx2080);

            Product hyper212 = new Product()
            {
                ProductName = "Hyper 212 EVO",
                Category = ProductCategory.Cooler,
                Manufacturer = "Cooler Master",
                Price = 10000,
                InStock = true,
                Quantity = 5,
                Description = "Szuper olcsó hűtő"
            };
            productlogic.AddProduct(rtx2080);

            Product ssd1 = new Product()
            {
                ProductName = "Samsung 500GB M.2 SSD",
                Category = ProductCategory.Storage,
                Manufacturer = "Samsung",
                Price = 20000,
                InStock = true,
                Quantity = 8,
                Description = "Szupergyors m.2 ssd a samsungtól"
            };
            productlogic.AddProduct(ssd1);

            Product psu1 = new Product()
            {
                ProductName = "Corsair 600W Gold",
                Category = ProductCategory.PowerSupply,
                Manufacturer = "Corsair",
                Price = 20000,
                InStock = true,
                Quantity = 23,
                Description = "600W-os arany értékelésű táp"
            };
            productlogic.AddProduct(psu1);
            #endregion
        }

        [HttpPost]
        public void CreateFullOrder([FromBody] ViewModel item)
        {
            orderlogic.GetOrder(item.OrderID).Customers.Add(customerlogic.GetCustomer(item.CustomerID));
            customerlogic.GetCustomer(item.CustomerID).Products.Add(productlogic.GetProduct(item.ProductID));

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public void DeleteOrderCustomers([FromBody] ViewModel item)
        {
            customerlogic.GetCustomer(item.CustomerID).Products = new List<Product>();
            orderlogic.GetOrder(item.OrderID).Customers = new List<Customer>();
        }
    }
}
