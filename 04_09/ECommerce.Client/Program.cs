using ECommerce.Api.Client.Core;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceUri = new Uri("fabric:/ECommerce/ECommerce.Api.Orders");
            var binding = WcfUtility.CreateTcpClientBinding();
            var servicePartitionResolver = ServicePartitionResolver.GetDefault();
            var wcfCommunicationClientFactory = 
                new WcfCommunicationClientFactory<IOrdersService>(binding, null, servicePartitionResolver);
            var servicePartitionClient = 
                new ServicePartitionClient<WcfCommunicationClient<IOrdersService>>(wcfCommunicationClientFactory, serviceUri);

            Console.WriteLine($"Please wait...");

            var result = await servicePartitionClient.InvokeWithRetryAsync(client => client.Channel.GetOrdersAsync());

            if (result!= null && result.Any())
            {
                foreach (var order in result)
                {
                    Console.WriteLine($"{order.Id} {order.CustomerId} {order.OrderDate} {order.Total}");
                }
            }
        }
    }
}