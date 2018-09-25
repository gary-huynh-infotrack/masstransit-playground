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
                var host = config.Host("rabbit host", "virtual host", h =>
                {
                    h.Username("username here");
                    h.Password("password here");
                });

                config.ReceiveEndpoint(host, "ordercreated_queue", e =>
                {
                    e.Consumer<OrderCreated3Consumer>();
                    e.Consumer<OrderCreated4Consumer>();
                });
            });
        }
    }
}
