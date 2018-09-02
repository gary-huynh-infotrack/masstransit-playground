using ClassLibrary1;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    public class SendEmailConsumer : IConsumer<SendEmail>
    {
        public async Task Consume(ConsumeContext<SendEmail> context)
        {
            Thread.Sleep(1000);
            await Console.Out.WriteLineAsync($"Email has been consumed, id: {context.Message.EmailId}");
        }
    }
}
