using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App
{
    public class SocketServer : ISocketServer
    {
        /// <summary>
        /// The permission
        /// </summary>
        private SocketPermission permission;

        /// <summary>
        /// The s listener
        /// </summary>
        private Socket sListener;

        /// <summary>
        /// The ip end point
        /// </summary>
        private IPEndPoint ipEndPoint;

        /// <summary>
        /// The handler
        /// </summary>
        private Socket handler;

        /// <summary>
        /// The log
        /// </summary>
        private ILog log;

        /// <summary>
        /// Occurs when [message recieved].
        /// </summary>
        public event EventHandler<string> MessageRecieved;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        public SocketServer()
        {
            this.log = log4net.LogManager.GetLogger
   (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Starts the specified ip address.
        /// </summary>
        /// <param name="ipAddress">The ip address or the hostname.</param>
        /// <param name="port">The port.</param>
        public void Start(string ipAddress = "", int port = 98765)
        {
            try
            {
                // Creates one SocketPermission object for access restrictions
                this.permission = new SocketPermission(
                 NetworkAccess.Accept,     // Allowed to accept connections 
                 TransportType.Tcp,        // Defines transport types 
                 "",                       // The IP addresses of local host 
                 SocketPermission.AllPorts // Specifies all ports 
                 );

                // Listening Socket object 
                this.sListener = null;

                // Ensures the code to have permission to access a Socket 
                this.permission.Demand();

                // Resolves a host name to an IPHostEntry instance 
                IPHostEntry ipHost = Dns.GetHostEntry(ipAddress);

                // Gets first IP address associated with a localhost 
                IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint 
                ipEndPoint = new IPEndPoint(ipAddr, port);

                // Create one Socket object to listen the incoming connection 
                sListener = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );

                // Associates a Socket with a local endpoint 
                sListener.Bind(ipEndPoint);

                this.log.Info("TCP socket started as a server.");

            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
        public void Listen()
        {
            try
            {
                // Places a Socket in a listening state and specifies the maximum 
                // Length of the pending connections queue 
                this.sListener.Listen(10);

                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                this.sListener.BeginAccept(aCallback, sListener);

                this.log.Info("Server is now listening on " + ipEndPoint.Address + " port: " + ipEndPoint.Port);
                ;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Accepts the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = null;

            // A new Socket to handle remote host communication 
            Socket handler = null;
            try
            {
                // Receiving byte array 
                byte[] buffer = new byte[1024];
                // Get Listening Socket object 
                listener = (Socket)ar.AsyncState;
                // Create a new socket 
                handler = listener.EndAccept(ar);

                // Using the Nagle algorithm 
                handler.NoDelay = false;

                // Creates one object array for passing data 
                object[] obj = new object[2];
                obj[0] = buffer;
                obj[1] = handler;

                // Begins to asynchronously receive data 
                handler.BeginReceive(
                    buffer,        // An array of type Byt for received data 
                    0,             // The zero-based position in the buffer  
                    buffer.Length, // The number of bytes to receive 
                    SocketFlags.None,// Specifies send and receive behaviors 
                    new AsyncCallback(ReceiveCallback),//An AsyncCallback delegate 
                    obj            // Specifies infomation for receive operation 
                    );

                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                listener.BeginAccept(aCallback, listener);
            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Receives the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Fetch a user-defined object that contains information 
                object[] obj = new object[2];
                obj = (object[])ar.AsyncState;

                // Received byte array 
                byte[] buffer = (byte[])obj[0];

                // A Socket to handle remote host communication. 
                handler = (Socket)obj[1];

                // Received message 
                string content = string.Empty;


                // The number of bytes received. 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    content += Encoding.UTF8.GetString(buffer, 0,
                        bytesRead);

                    // If message contains "<Client Quit>", finish receiving
                    if (!string.IsNullOrEmpty(content))
                    {
                        // Convert byte array to string
                        this.MessageRecieved?.Invoke(this, content);
                    }
                    else
                    {
                        // Continues to asynchronously receive data
                        byte[] buffernew = new byte[1024];
                        obj[0] = buffernew;
                        obj[1] = handler;
                        handler.BeginReceive(buffernew, 0, buffernew.Length,
                            SocketFlags.None,
                            new AsyncCallback(ReceiveCallback), obj);
                    }
                }
            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Sends to clients.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendToClients(string message)
        {
            try
            {
                // Convert byte array to string 
                string str = message;

                // Prepare the reply message 
                byte[] byteData =
                    Encoding.Unicode.GetBytes(str);

                // Sends data asynchronously to a connected Socket 
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);

            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Sends the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // A Socket which has sent the data to remote host 
                Socket handler = (Socket)ar.AsyncState;

                // The number of bytes sent to the Socket 
                int bytesSend = handler.EndSend(ar);
                Console.WriteLine(
                    "Sent {0} bytes to Client", bytesSend);
            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Sends to clients the object as JSON string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageObject">The message object.</param>
        public void SendToClients<T>(T messageObject)
        {
            try
            {
                // Convert byte array to string 
                string str = JsonConvert.SerializeObject(messageObject);

                // Prepare the reply message 
                byte[] byteData =
                    Encoding.Unicode.GetBytes(str);

                // Sends data asynchronously to a connected Socket 
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), handler);

            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Shuts down.
        /// </summary>
        public void ShutDown()
        {
            try
            {
                if (sListener.Connected)
                {
                    sListener.Shutdown(SocketShutdown.Receive);
                    sListener.Close();
                }
            }
            catch (Exception exc)
            {
                this.log.Error("Error Occured: " + exc.Message);
            }
        }
    }
}
