using Core;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver1
{
    public class OrderCreated1Consumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Thread.Sleep(1000);
            await Console.Out.WriteLineAsync($"Order Created event consumer 1 has been consumed, id: {context.Message.EmailId}");
        }
    }
}
