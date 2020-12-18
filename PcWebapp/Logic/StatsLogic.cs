using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Logic
{
    public class StatsLogic
    {
        IRepository<Order> orderrepo;
        IRepository<Customer> customerrepo;
        IRepository<Product> productrepo;

        public StatsLogic(IRepository<Order> orderRepo, IRepository<Customer> customerRepo, IRepository<Product> productRepo)
        {
            this.orderrepo = orderRepo;
            this.customerrepo = customerRepo;
            this.productrepo = productRepo;
        }

        public IEnumerable<Order> ExpensiveOrders()
        {
            var eo = from order in orderrepo.Read().ToList()
                     join customer in customerrepo.Read().ToList() on order.OrderID equals customer.Order.OrderID
                     join product in productrepo.Read().ToList() on customer.CustomerID equals product.CustomerID
                     where (order.OrderedQuantity * product.Price) > 100000
                     select order;
            return eo;
        }

        public IEnumerable<Product> LongestUserOrders()
        {
            var luo = (from product in productrepo.Read().ToList()
                       join customer in customerrepo.Read().ToList()
                       on product.CustomerID equals customer.CustomerID
                       orderby customer.RegDate descending
                       select customer).FirstOrDefault().Products;
            return luo;
        }
    }
}
