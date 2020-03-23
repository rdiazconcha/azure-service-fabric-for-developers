using ECommerce.Api.Client.Core;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Orders : StatelessService, IOrdersService
    {
        public Orders(StatelessServiceContext context)
            : base(context)
        { }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = new List<Order>();
            orders.Add(new Order() { Id = 1, CustomerId = 1, 
                Total = 1000, OrderDate = DateTime.Now.Date });
            orders.Add(new Order() { Id = 2, CustomerId = 2, 
                Total = 2000, OrderDate = DateTime.Now.Date.AddDays(-1) });
            return Task.FromResult(orders.AsEnumerable());
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
           {
                new ServiceInstanceListener((context) =>
                    new WcfCommunicationListener<IOrdersService>(context, this, 
                    WcfUtility.CreateTcpListenerBinding(), "ServiceEndpoint"))
           };
        }

    }
}