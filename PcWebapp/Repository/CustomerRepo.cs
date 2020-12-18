using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Repository
{
    public class CustomerRepo : IRepository<Customer>
    {
        PCStoreContext ctx = new PCStoreContext();
        public void Add(Customer customer)
        {
            ctx.Customers.Add(customer);
            Save();
        }

        public void Delete(Customer customer)
        {
            ctx.Customers.Remove(customer);
            Save();
        }

        public void Delete(string CusID)
        {
            Delete(Read(CusID));
        }

        public Customer Read(string CusID)
        {
            return ctx.Customers.FirstOrDefault(x => x.CustomerID == CusID);
        }

        public IQueryable<Customer> Read()
        {
            return ctx.Customers.AsQueryable();
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(string oldCustomerId, Customer newCustomer)
        {
            var oldCustomer = Read(oldCustomerId);
            oldCustomer.CustomerName = newCustomer.CustomerName;
            oldCustomer.EmailAddress = newCustomer.EmailAddress;
            oldCustomer.PhoneNumber = newCustomer.PhoneNumber;
            oldCustomer.RegDate = newCustomer.RegDate;
            oldCustomer.Address = newCustomer.Address;
            oldCustomer.Password = newCustomer.Password;
            oldCustomer.Order = newCustomer.Order;
            Save();
        }
    }
}