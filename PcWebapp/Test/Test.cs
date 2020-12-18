using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using Repository;
using Models;
using Logic;
using System.Linq;

namespace Test
{
    [TestFixture]
    class Test
    {
        public Mock<IRepository<Order>> orderrepo = new Mock<IRepository<Order>>();
        public Mock<IRepository<Customer>> customerrepo = new Mock<IRepository<Customer>>();
        public Mock<IRepository<Product>> productrepo = new Mock<IRepository<Product>>();

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
            customerrepo.Verify(x => x.Read(), Times.Once);
        }
        [Test]
        public void UpdateTest()
        {
            productrepo.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<Product>()));
            ProductLogic logic = new ProductLogic(productrepo.Object, customerrepo.Object, orderrepo.Object);
            logic.UpdateProduct("ID1", new Product() { ProductID = "ID1" });
            productrepo.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Product>()), Times.Once);
        }
    }
}
