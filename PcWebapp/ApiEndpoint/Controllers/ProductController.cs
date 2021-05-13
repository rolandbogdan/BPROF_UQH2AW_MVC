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
    public class ProductController : ControllerBase
    {
        ProductLogic logic;

        public ProductController(ProductLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteProduct(string uid)
        {
            logic.DeleteProduct(uid);
        }

        [HttpGet("{uid}")]
        public Product GetProduct(string uid)
        {
            return logic.GetProduct(uid);  
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts(string uid)
        {
            return logic.GetAllProducts();
        }

        [HttpPost]
        public void AddProduct([FromBody] Product item)
        {
            logic.AddProduct(item);
        }

        [HttpPut("{oldid}")]
        public void UpdateVideo(string oldid, [FromBody] Product item)
        {
            logic.UpdateProduct(oldid, item);
        }
    }
}
