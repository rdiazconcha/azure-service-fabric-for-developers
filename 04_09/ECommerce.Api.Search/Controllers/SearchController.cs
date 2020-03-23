using ECommerce.Api.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
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

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var serviceName = "fabric:/ECommerce/ECommerce.Api.Customers";
            var uri = await ResolveAsync(serviceName);

            try
            {
                var client = new HttpClient() { BaseAddress = uri };
                var result = await client.GetAsync($"api/customers/{id}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var customer = JsonSerializer.Deserialize<dynamic>(content);
                    return Ok(customer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<Uri> ResolveAsync(string name)
        {
            var uri = new Uri(name);
            var resolver = ServicePartitionResolver.GetDefault();
            var service = await resolver.ResolveAsync(uri, ServicePartitionKey.Singleton, 
                        CancellationToken.None);
            var addresses = JObject.Parse(service.GetEndpoint().Address);
            var primary = (string)addresses["Endpoints"].First();
            return new Uri(primary);
        }
    }
}