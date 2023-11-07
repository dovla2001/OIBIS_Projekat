using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimarnaAGS
{
    public class WCFService : IWCFContracts, ISecurityService
    {
        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }

        public void CheckUser(string username, string password)
        {
            Console.WriteLine("Novi korisnik je " + username + ", sifra korisnika je " + password);
        }
    }
}
