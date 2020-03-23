using ECommerce.Api.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var service = ServiceProxy.Create<IProductsService>(new Uri("fabric:/ECommerce/ECommerce.Api.Products"));
            var product = await service.GetProductAsync(id);
            if (product.IsSuccess)
            {
                return Ok(product.Product);
            }
            return NotFound();
        }
    }
}
