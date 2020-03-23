using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var result = new Customer() { Id = id, Name = $"Customer {id}" };
            return Ok(result);
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}