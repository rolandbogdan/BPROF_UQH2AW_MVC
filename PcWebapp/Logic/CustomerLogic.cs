﻿using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class CustomerLogic
    {
        IRepository<Product> productRepo;
        IRepository<Customer> customerRepo;
        IRepository<Order> orderRepo;
        public CustomerLogic(IRepository<Product> productrepo, IRepository<Customer> customerrepo, IRepository<Order> orderrepo)
        {
            this.productRepo = productrepo;
            this.customerRepo = customerrepo;
            this.orderRepo = orderrepo;
        }
        public void AddCustomer(Customer customer)
        {
            if (!Contains(customer.CustomerID))
                this.customerRepo.Add(customer);

        }
        public void DeleteCustomer(string customerID)
        {
            this.customerRepo.Delete(customerID);
        }
        public IQueryable<Customer> GetAllCustomers()
        {
            return customerRepo.Read();
        }
        public Customer GetCustomer(string customerID)
        {
            return customerRepo.Read(customerID);
        }
        public void UpdateCustomer(string oldcustomerID, Customer newCustomer)
        {
            customerRepo.Update(oldcustomerID, newCustomer);
        }
        public bool Contains(string customerID)
        {
            foreach (var item in this.GetAllCustomers())
                if (item.CustomerID == customerID)
                    return true;
            return false;
        }
    }
}
