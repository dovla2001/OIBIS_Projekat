using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using System.ServiceModel.Channels;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ITest> channelTest = new ChannelFactory<ITest>("Test");
            ITest proxyTest = channelTest.CreateChannel();

            string tekst = proxyTest.Proba();
            Console.WriteLine(tekst);
            Console.Read();
        }
    }
}
