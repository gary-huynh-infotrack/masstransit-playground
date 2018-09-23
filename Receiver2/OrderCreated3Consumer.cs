using Core;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver2
{
    public class OrderCreated3Consumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            Thread.Sleep(1000);
            await Console.Out.WriteLineAsync($"Order Created event consumer 3 has been consumed, id: {context.Message.EmailId}");
        }
    }
}
