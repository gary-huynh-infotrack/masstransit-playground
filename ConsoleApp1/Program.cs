using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;
using ClassLibrary1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IBusControl bus = ConfigureBus();

            bus.Start();

            Console.WriteLine("Publisher started");
            for (;;)
            {
                var consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (consoleKeyInfo.Key == ConsoleKey.F)
                {
                    //.. send fax
                    var messageId = Guid.NewGuid().ToString();
                    Console.WriteLine($"Fax sent: {messageId}");
                    bus.Publish(new SendFax()
                    {
                        FaxId = messageId,
                        Body = "This is an email body",
                        From = "gary.huynh@gmail.com",
                        To = "hihi@gmail.com"
                    });
                }

                if (consoleKeyInfo.Key == ConsoleKey.E)
                {
                    //.. send email
                    var messageId = Guid.NewGuid().ToString();
                    Console.WriteLine($"Email sent: {messageId}");
                    bus.Publish(new SendEmail()
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

                config.Message<SendEmail>(x => 
                {
                    x.SetEntityName("send_email_entity");
                });
                config.Message<SendFax>(x =>
                {
                    x.SetEntityName("send_fax_entity");
                });
                config.Publish<SendEmail>(x =>
                {
                    x.BindQueue("messaging_exchange", "send_email_queue");
                });
                config.Publish<SendFax>(x =>
                {
                    x.BindQueue("messaging_exchange", "send_fax_queue");
                });
            });
        }
    }
}
