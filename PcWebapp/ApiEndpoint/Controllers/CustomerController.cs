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
        public void AddCustomer([FromBody] Customer item)
        {
            logic.AddCustomer(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateCustomer(string oldid, [FromBody] Customer item)
        {
            logic.UpdateCustomer(oldid, item);
        }
    }
}
