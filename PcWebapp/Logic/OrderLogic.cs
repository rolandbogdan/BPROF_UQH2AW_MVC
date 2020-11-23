using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    class OrderLogic
    {
        IRepository<Product> productRepo;
        IRepository<Customer> customerRepo;
        IRepository<Order> orderRepo;
        public OrderLogic(IRepository<Product> productrepo, IRepository<Customer> customerrepo, IRepository<Order> orderrepo)
        {
            this.productRepo = productrepo;
            this.customerRepo = customerrepo;
            this.orderRepo = orderrepo;
        }
        public void AddOrder(Order order)
        {
            this.orderRepo.Add(order);
        }
        public void DeleteOrder(string orderID)
        {
            this.orderRepo.Delete(orderID);
        }
        public IQueryable<Order> GetAllOrders()
        {
            return orderRepo.Read();
        }
        public Order GetOrder(string orderID)
        {
            return orderRepo.Read(orderID);
        }
        public void UpdateOrder(string oldorderID, Order newOrder)
        {
            orderRepo.Update(oldorderID, newOrder);
        }
    }
}
