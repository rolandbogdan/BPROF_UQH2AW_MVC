using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class OrderRepo : IRepository<Order>
    {
        PCStoreContext ctx = new PCStoreContext();
        public void Add(Order order)
        {
            ctx.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            ctx.Orders.Remove(order);
        }

        public void Delete(string OrderID)
        {
            Delete(Read(OrderID));
        }

        public Order Read(string OrdID)
        {
            return ctx.Orders.FirstOrDefault(x => x.OrderID == OrdID);
        }

        public IQueryable<Order> Read()
        {
            return ctx.Orders.AsQueryable();
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(string oldOrderID, Order newOrder)
        {
            var oldOrder = Read(oldOrderID);
            //quantity, date, comment, status
            oldOrder.OrderedQuantity = newOrder.OrderedQuantity;
            oldOrder.OrderDate = newOrder.OrderDate;
            oldOrder.Comment = newOrder.Comment;
            oldOrder.OrderStatus = newOrder.OrderStatus;
            Save();
        }
    }
}
