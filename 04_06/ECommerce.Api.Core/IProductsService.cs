using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace ECommerce.Api.Core
{
    public interface IProductsService : IService
    {
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
