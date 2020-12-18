using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class OrderLogic
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
            if (!Contains(order.OrderID))
                this.orderRepo.Add(order);
        }
        public void DeleteOrder(string orderID)
        {
            this.orderRepo.Delete(orderID);
            this.orderRepo.Save();
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
        public bool Contains(string orderID)
        {
            foreach (var item in this.GetAllOrders())
                if (item.OrderID == orderID)
                    return true;
            return false;
        }
    }
}
