using Contracts;
using Manager;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace PrimarnaAGS
{
    public class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = Formater.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();
            NetTcpBinding bindingWindows = new NetTcpBinding();

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            bindingWindows.Security.Mode = SecurityMode.Transport;
            bindingWindows.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            bindingWindows.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            string address = "net.tcp://localhost:9999/ISecurityService";
            string addressWindows = "net.tcp://localhost:9998/ISecurityService";

            ServiceHost host = new ServiceHost(typeof(WCFService));

            host.AddServiceEndpoint(typeof(IWCFContracts), binding, address);
            host.AddServiceEndpoint(typeof(IWCFContracts), bindingWindows, addressWindows);

            try
            {
                host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
                host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
                host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
                host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

                host.Open();
                Console.WriteLine("WCFService is started. Press <enter> to stop ...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host.Close();
            }


        }
    }
}
