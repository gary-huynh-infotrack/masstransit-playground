using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Receiver1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====== Starting Receiver 1 ======");

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

                config.ReceiveEndpoint(host, "emailclientnotification_queue", e => 
                {
                    e.Consumer<OrderCreated1Consumer>();
                    e.Consumer<OrderCreated2Consumer>();
                });
            });
        }
    }
}
