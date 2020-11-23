using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ProductLogic
    {
        //Miért kell az összes? Nem értem:(
        IRepository<Product> productRepo;
        IRepository<Customer> customerRepo;
        IRepository<Order> orderRepo;

        public ProductLogic(IRepository<Product> productrepo, IRepository<Customer> customerrepo, IRepository<Order> orderrepo)
        {
            this.productRepo = productrepo;
            this.customerRepo = customerrepo;
            this.orderRepo = orderrepo;
        }

        public void AddProduct(Product product)
        {
            this.productRepo.Add(product);
        }
        public void DeleteProduct(string productID)
        {
            this.productRepo.Delete(productID);
        }
        public IQueryable<Product> GetAllProducts()
        {
            return productRepo.Read();
        }

        public Product GetProduct(string productID)
        {
            return productRepo.Read(productID);
        }
        public void UpdateProduct(string oldProductID, Product newProduct)
        {
            productRepo.Update(oldProductID, newProduct);
        }
    }
}
