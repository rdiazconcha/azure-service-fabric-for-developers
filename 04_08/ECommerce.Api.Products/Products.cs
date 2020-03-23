using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Api.Core;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
namespace ECommerce.Api.Products
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Products : StatelessService, IProductsService
    {
        public Products(StatelessServiceContext context)
            : base(context)
        { }

        public Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            return Task.FromResult(((bool, Product, string))(true, new Product() { Id = id, Name = $"Product {id}"}, null));
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var listeners = this.CreateServiceRemotingInstanceListeners();
            return listeners;
        }
       
    }
}
