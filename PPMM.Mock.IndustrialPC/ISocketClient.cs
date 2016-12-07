using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Mock.IndustrialPC
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISocketClient
    {
        /// <summary>
        /// Connects the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        void Connect(string ipAddress = "", int port = 98765);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(string message);

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        event EventHandler<string> MessageRecieved;
    }
}
