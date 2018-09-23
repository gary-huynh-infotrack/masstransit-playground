using Core;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiver2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====== Starting Receiver 2 ======");
            var bus = ConfigureBus();
            bus.Start();


        }
        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.MessageTopology.SetEntityNameFormatter(new NameFormatter());

                config.ReceiveEndpoint(host, "reportdatasync_queue", e =>
                {
                    e.Consumer<OrderCreated3Consumer>();
                    e.Consumer<OrderCreated4Consumer>();
                });
            });
        }
    }
}
