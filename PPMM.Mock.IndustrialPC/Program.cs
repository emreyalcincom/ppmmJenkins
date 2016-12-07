using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Mock.IndustrialPC
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";

            List<string> sampleMessages = new List<string>();
            sampleMessages.Add("1234" + "$" + "0");
            sampleMessages.Add("1235" + "$" + "1");
            sampleMessages.Add("1236" + "$" + "3");
            sampleMessages.Add("1237" + "$" + "2");
            sampleMessages.Add("1238" + "$" + "4");

            SocketClient client = new SocketClient();
            client.Connect(ip, 4544);

            foreach (var message in sampleMessages)
            {
                client.Send(message);
            }
        }
    }
}
