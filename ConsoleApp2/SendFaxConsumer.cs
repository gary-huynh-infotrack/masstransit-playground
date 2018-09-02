using ClassLibrary1;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    public class SendFaxConsumer : IConsumer<SendFax>
    {
        public async Task Consume(ConsumeContext<SendFax> context)
        {
            Thread.Sleep(1000);
            await Console.Out.WriteLineAsync($"Fax has been consumed, id: {context.Message.FaxId}");   
        }
    }
}
