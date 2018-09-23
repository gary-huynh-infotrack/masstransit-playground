using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====== Starting Publisher ======");

            IBusControl bus = ConfigureBus();

            bus.Start();

            for (;;)
            {
                var consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (consoleKeyInfo.Key == ConsoleKey.E)
                {
                    //.. send email
                    var messageId = Guid.NewGuid().ToString();
                    Console.WriteLine($"Order Created event sent: {messageId}");
                    bus.Publish(new OrderCreated()
                    {
                        EmailId = messageId,
                        Body = "This is an email body",
                        From = "gary.huynh@gmail.com",
                        To = "hihi@gmail.com"
                    });
                }
            }          
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(config => 
            {
                var host = config.Host(new Uri("rabbitmq://localhost/"), h => 
                {
                    h.PublisherConfirmation = true;
                    h.Username("guest");
                    h.Password("guest");
                });
                config.MessageTopology.SetEntityNameFormatter(new NameFormatter());
                
            });
        }
    }
}
