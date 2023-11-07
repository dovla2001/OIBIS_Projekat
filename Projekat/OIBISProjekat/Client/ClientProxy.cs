using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    
        public class ClientProxy : ChannelFactory<ISecurityService>, ISecurityService, IDisposable
        {
            ISecurityService factory;

            public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
            {
                factory = this.CreateChannel();
            }

            public void CheckUser(string username, string password)
            {

                try
                {
                    factory.CheckUser(username, password);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
        }
    }

