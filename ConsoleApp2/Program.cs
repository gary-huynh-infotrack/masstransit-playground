using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;
using ClassLibrary1;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
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
                config.Message<SendEmail>(x =>
                {
                    x.SetEntityName("send_email_entity");
                });
                config.Message<SendFax>(x =>
                {
                    x.SetEntityName("send_fax_entity");
                });
                config.ReceiveEndpoint(host, "send_email_queue", e => 
                {
                    e.Bind("messaging_exchange");
                    e.Consumer<SendEmailConsumer>();
                });
                config.ReceiveEndpoint(host, "send_fax_queue", e =>
                {
                    e.Bind("messaging_exchange");
                    e.Consumer<SendFaxConsumer>();
                });
            });
        }
    }
}
