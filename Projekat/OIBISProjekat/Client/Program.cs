using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9998/ISecurityService";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            using (ClientProxy proxy = new ClientProxy(binding, address))
            {
                proxy.CheckUser("pjur", "pjur369");
            }
            string user = WindowsIdentity.GetCurrent().Name;
            user = user.Split('\\')[1];
            Console.WriteLine($"Prijavljeni korisnik:{user}");
            Console.Read();
        }
    }
}
