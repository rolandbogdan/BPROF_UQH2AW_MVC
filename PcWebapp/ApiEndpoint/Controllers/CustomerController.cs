using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class CustomerController : ControllerBase
    {
        CustomerLogic logic;

        public CustomerController(CustomerLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteCustomer(string uid)
        {
            logic.DeleteCustomer(uid);
        }

        [HttpGet("{uid}")]
        public Customer GetCustomer(string uid)
        {
            return logic.GetCustomer(uid);
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers(string uid)
        {
            return logic.GetAllCustomers();
        }

        [HttpPost]
        public void AddProduct([FromBody] Customer item)
        {
            logic.AddCustomer(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateVideo(string oldid, [FromBody] Customer item)
        {
            logic.UpdateCustomer(oldid, item);
        }
    }
}
