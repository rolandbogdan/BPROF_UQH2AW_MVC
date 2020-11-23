using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ProductRepo : IRepository<Product>
    {
        PCStoreContext ctx = new PCStoreContext();

        public void Add(Product item)
        {
            ctx.Products.Add(item);
            Save();
        }

        public void Delete(Product item)
        {
            ctx.Products.Remove(item);
            Save();
        }

        public void Delete(string productid)
        {
            Delete(Read(productid));
        }

        public Product Read(string productid)
        {
            return ctx.Products.FirstOrDefault(x => x.ProductID == productid);
        }

        public IQueryable<Product> Read()
        {
            return ctx.Products.AsQueryable();
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(string oldProductid, Product newProduct)
        {
            var oldProduct = Read(oldProductid);

            oldProduct.ProductName = newProduct.ProductName;
            oldProduct.Category = newProduct.Category;
            oldProduct.Manufacturer = newProduct.Manufacturer;
            oldProduct.Price = newProduct.Price;
            oldProduct.InStock = newProduct.InStock;
            oldProduct.Quantity = newProduct.Quantity;
            oldProduct.Description = newProduct.Description;
            Save();
        }
    }
}
