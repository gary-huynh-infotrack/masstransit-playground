using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Topology;
using ClassLibrary1;
using MassTransit;

namespace ConsoleApp1
{
    public class SendEmailTopology : IMessagePublishTopology<SendEmail>
    {
        public void Apply(ITopologyPipeBuilder<PublishContext<SendEmail>> builder)
        {
            throw new NotImplementedException();
        }

        public bool TryGetPublishAddress(Uri baseAddress, out Uri publishAddress)
        {
            throw new NotImplementedException();
        }
    }
}
