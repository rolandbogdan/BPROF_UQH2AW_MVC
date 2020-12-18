using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using Repository;
using Models;
using Logic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Test
{
    [TestFixture]
    class Test
    {
        public Mock<IRepository<Order>> orderrepo = new Mock<IRepository<Order>>();
        public Mock<IRepository<Customer>> customerrepo = new Mock<IRepository<Customer>>();
        public Mock<IRepository<Product>> productrepo = new Mock<IRepository<Product>>();

        /*  Valamiért ha run all-al tesztelem elsőre, a non crud-ok hibát dobnak,
            de egyesével működnek szépen.*/
        [Test]
        public void AddTest()
        {
            orderrepo.Setup(x => x.Add(It.IsAny<Order>()));
            OrderLogic logic = new OrderLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            logic.AddOrder(new Order());
            orderrepo.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);
        }
        [Test]
        public void DeleteTest()
        {
            customerrepo.Setup(x => x.Delete(It.IsAny<string>()));
            CustomerLogic logic = new CustomerLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            logic.DeleteCustomer("ID1");
            customerrepo.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }
        [Test]
        public void ReadTest()
        {
            productrepo.Setup(x => x.Read(It.IsAny<string>())).Returns(
                new Product() { ProductID = "ID01" });
            ProductLogic logic = new ProductLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            Product result = logic.GetProduct("ID01");
            Assert.That(result.ProductID, Is.EqualTo("ID01"));
            productrepo.Verify(x => x.Read(It.IsAny<string>()), Times.Once);
        }
        [Test]
        public void ReadAllTest()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer() {CustomerID = "ID1"},
                new Customer() {CustomerID = "ID2"},
                new Customer() {CustomerID = "ID2"},
            };
            customerrepo.Setup(x => x.Read()).Returns(customers.AsQueryable());
            CustomerLogic logic = new CustomerLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            var result = logic.GetAllCustomers();

            Assert.That(result, Is.EquivalentTo(customers));
            customerrepo.Verify(x => x.Read(), Times.AtLeastOnce);
        }
        [Test]
        public void UpdateTest()
        {
            productrepo.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Product>()));
            ProductLogic logic = new ProductLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            logic.UpdateProduct("ID1", new Product() { ProductID = "ID1" });
            productrepo.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void ExpensiveOrdersTest()
        {
            List<Order> orders = new List<Order>()
            {
                new Order() {OrderID = "OID1", OrderedQuantity=1},
                new Order() {OrderID = "OID2", OrderedQuantity=3},
                new Order() {OrderID = "OID3", OrderedQuantity=2}
            };
            List<Customer> customers = new List<Customer>()
            {
                new Customer() {CustomerID = "CID1", Order = orders[0]},
                new Customer() {CustomerID = "CID2", Order = orders[1]},
                new Customer() {CustomerID = "CID3", Order = orders[2]}
            };
            List<Product> products = new List<Product>()
            {
                new Product() {ProductID = "PID1", Price = 110000, CustomerID = "CID1"},
                new Product() {ProductID = "PID2", Price = 20000, CustomerID = "CID2"},
                new Product() {ProductID = "PID3", Price = 45000, CustomerID = "CID3"}
            };

            List<Order> expected = new List<Order>();
            expected.Add(orders[0]);

            orderrepo.Setup(x => x.Read()).Returns(orders.AsQueryable());
            customerrepo.Setup(x => x.Read()).Returns(customers.AsQueryable());
            productrepo.Setup(x => x.Read()).Returns(products.AsQueryable());

            StatsLogic logic = new StatsLogic(orderrepo.Object, customerrepo.Object, productrepo.Object);

            var result = logic.ExpensiveOrders();

            Assert.That(result, Is.EquivalentTo(expected));
            orderrepo.Verify(x => x.Read(), Times.AtLeastOnce);
            customerrepo.Verify(x => x.Read(), Times.AtLeastOnce);
            productrepo.Verify(x => x.Read(), Times.AtLeastOnce);

        }
        [Test]
        public void LongestUserOrdersTest()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer() {CustomerID = "CID1", RegDate=new DateTime(2015, 1, 1),},
                new Customer() {CustomerID = "CID2", RegDate=new DateTime(2010, 1, 1)},
                new Customer() {CustomerID = "CID3", RegDate=new DateTime(2020, 1, 1)}
            };
            List<Product> products = new List<Product>()
            {
                new Product() {ProductID="PID1",CustomerID="CID1"},
                new Product() {ProductID="PID2",CustomerID="CID2"},
                new Product() {ProductID="PID3",CustomerID="CID3"}
            };
            customers[0].Products = new List<Product>();
            customers[0].Products.Add(products[0]);
            customers[1].Products = new List<Product>();
            customers[1].Products.Add(products[1]);
            customers[2].Products = new List<Product>();
            customers[2].Products.Add(products[2]);

            List<Product> expected = new List<Product>();
            expected.Add(products[1]);

            customerrepo.Setup(x => x.Read()).Returns(customers.AsQueryable());
            productrepo.Setup(x => x.Read()).Returns(products.AsQueryable());

            StatsLogic logic = new StatsLogic(orderrepo.Object, customerrepo.Object, productrepo.Object);

            var result = logic.LongestUserOrders();
            
            Assert.That(result, Is.EquivalentTo(expected));
            customerrepo.Verify(x => x.Read(), Times.AtLeastOnce);
            productrepo.Verify(x => x.Read(), Times.AtLeastOnce);
        }
        [Test]
        public void CustomersOfManufacturerTest()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer() {CustomerID = "CID1", Products = new List<Product>()},
                new Customer() {CustomerID = "CID2", Products = new List<Product>()},
                new Customer() {CustomerID = "CID3", Products = new List<Product>()}
            };
            List<Product> products = new List<Product>()
            {
                new Product() {ProductID="PID1",CustomerID="CID1",Manufacturer="Test1"},
                new Product() {ProductID="PID2",CustomerID="CID2",Manufacturer="Test2"},
                new Product() {ProductID="PID3",CustomerID="CID3",Manufacturer="Test3"}
            };
            customers[0].Products.Add(products[0]);
            customers[1].Products.Add(products[1]);
            customers[2].Products.Add(products[2]);

            List<Customer> expected = new List<Customer>();
            expected.Add(customers[2]);

            customerrepo.Setup(x => x.Read()).Returns(customers.AsQueryable());
            productrepo.Setup(x => x.Read()).Returns(products.AsQueryable());

            StatsLogic logic = new StatsLogic(orderrepo.Object, customerrepo.Object, productrepo.Object);

            var result = logic.CustomersOfManufacturer("Test3");

            Assert.That(result, Is.EquivalentTo(expected));
            customerrepo.Verify(x => x.Read(), Times.Once);
            productrepo.Verify(x => x.Read(), Times.Once);
        }
    }
}
