using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [Authorize]
    [ApiController]
    [Route("{controller}")]
    public class OrderController : ControllerBase
    {
        OrderLogic logic;

        public OrderController(OrderLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteOrder(string uid)
        {
            logic.DeleteOrder(uid);
        }

        [HttpGet("{uid}")]
        public Order GetOrder(string uid)
        {
            return logic.GetOrder(uid);
        }

        [HttpGet]
        public IEnumerable<Order> GetAllOrders(string uid)
        {
            return logic.GetAllOrders();
        }

        [HttpPost]
        public void AddProduct([FromBody] Order item)
        {
            logic.AddOrder(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateVideo(string oldid, [FromBody] Order item)
        {
            logic.UpdateOrder(oldid, item);
        }
    }
}
