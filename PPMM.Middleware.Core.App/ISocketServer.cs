using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISocketServer
    {
        /// <summary>
        /// Starts the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        void Start(string ipAddress = "", int port = 98765);

        /// <summary>
        /// Shuts down.
        /// </summary>
        void ShutDown();

        /// <summary>
        /// Listens this instance.
        /// </summary>
        void Listen();

        /// <summary>
        /// Sends to clients.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendToClients(string message);

        /// <summary>
        /// Sends to clients.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageObject">The message object.</param>
        void SendToClients<T>(T messageObject);

        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        event EventHandler<string> MessageRecieved;
    }
}
