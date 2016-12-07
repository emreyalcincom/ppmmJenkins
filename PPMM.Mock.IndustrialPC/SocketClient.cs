using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Mock.IndustrialPC
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PPMM.Mock.IndustrialPC.ISocketClient" />
    public class SocketClient : ISocketClient
    {
        // Receiving byte array  
        byte[] bytes = new byte[1024];

        /// <summary>
        /// The sender sock
        /// </summary>
        Socket senderSock;

        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        public event EventHandler<string> MessageRecieved;

        /// <summary>
        /// Connects the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        public void Connect(string ipAddress = "", int port = 98765)
        {
            try
            {
                // Create one SocketPermission for socket access restrictions 
                SocketPermission permission = new SocketPermission(
                    NetworkAccess.Connect,    // Connection permission 
                    TransportType.Tcp,        // Defines transport types 
                    "",                       // Gets the IP addresses 
                    SocketPermission.AllPorts // All ports 
                    );

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance            
                IPHostEntry ipHost = Dns.GetHostEntry(ipAddress);

                // Gets first IP address associated with a localhost 
                IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint 
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                // Create one Socket object to setup Tcp connection 
                this.senderSock = new Socket(
                    ipAddr.AddressFamily,// Specifies the addressing scheme 
                    SocketType.Stream,   // The type of socket  
                    ProtocolType.Tcp     // Specifies the protocols  
                    );

                this.senderSock.NoDelay = false;   // Using the Nagle algorithm 

                // Establishes a connection to a remote host 
                this.senderSock.Connect(ipEndPoint);
                Console.WriteLine("Socket connected to " + senderSock.RemoteEndPoint.ToString());

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                // Disables sends and receives on a Socket. 
                senderSock.Shutdown(SocketShutdown.Both);

                //Closes the Socket connection and releases all resources 
                senderSock.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message)
        {
            try
            {
                // Sending message 
                //<Client Quit> is the sign for end of data 
                byte[] msg = Encoding.UTF8.GetBytes(message);

                // Sends data to a connected Socket. 
                int bytesSend = this.senderSock.Send(msg);

                ReceiveDataFromServer();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Receives the data from server.
        /// </summary>
        private void ReceiveDataFromServer()
        {
            try
            {
                // Receives data from a bound Socket. 
                int bytesRec = this.senderSock.Receive(bytes);

                // Converts byte array to string 
                String theMessageToReceive = Encoding.Unicode.GetString(bytes, 0, bytesRec);

                // Continues to read the data till data isn't available 
                while (this.senderSock.Available > 0)
                {
                    bytesRec = this.senderSock.Receive(bytes);
                    theMessageToReceive += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                }

                this.MessageRecieved?.Invoke(this, theMessageToReceive);
            }
            catch (Exception exc)
           {
                Console.WriteLine(exc.ToString());
            }
        }
    }
}
