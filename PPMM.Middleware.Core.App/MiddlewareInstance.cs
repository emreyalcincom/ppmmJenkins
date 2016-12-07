using log4net;
using Microsoft.Owin.Hosting;
using PPMM.Data.Model.Entities;
using PPMM.Data.Model.Enumerations;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace PPMM.Middleware.Core.App
{
    /// <summary>
    /// 
    /// </summary>
    public class MiddlewareInstance
    {
        /// <summary>
        /// The server
        /// </summary>
        private SocketServer server;

        /// <summary>
        /// The batch operator
        /// </summary>
        private BatchOperator batchOperator;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The erp client
        /// </summary>
        private FNSSErpService.FnssErpServiceClient erpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewareInstance" /> class.
        /// </summary>
        public MiddlewareInstance()
        {
            string ipAddress = ConfigurationManager.AppSettings["IpAddress"];
            string portAddress = ConfigurationManager.AppSettings["Port"];
            int port = int.MaxValue;

            if (int.TryParse(portAddress, out port))
            {
                this.server = new SocketServer();
                this.server.Start(ipAddress, port);

                this.erpClient = new FNSSErpService.FnssErpServiceClient();
                this.batchOperator = new BatchOperator(new TimeSpan(24, 0, 0, 0, 0));

                this.log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                this.InitializeOwinHost();

                log.Info("Middleware instance Fully initialized.");
            }
        }

        private void InitializeOwinHost()
        {
            string baseAddress = "http://localhost:9123/";

            WebApp.Start<Startup>(url: baseAddress);

            log.Info("Self hosted owin app initialized on address: " + baseAddress);
        }

        /// <summary>
        /// Stops the owin host.
        /// </summary>
        private void StopOwinHost()
        {
        }

        /// <summary>
        /// Starts the working.
        /// </summary>
        public void StartWorking()
        {
            this.server.Listen();
            this.server.MessageRecieved += this.Server_MessageRecieved;
            this.batchOperator.Start();
            log.Info("Middleware instance started to working.");
        }

        /// <summary>
        /// Servers the message recieved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Server_MessageRecieved(object sender, string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                return;
            }

            var splitted = e.Split('$');

            if (splitted.Length != 2)
            {
                this.log.Error("Data Format is not correct. The length must be 2 with splitter $!");
                return;
            }

            string ShiftPrimaryKey = splitted.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ShiftPrimaryKey))
            {
                this.log.Error("Shift Primary key cannot be whitespace or null!");
                return;
            }

            dynamic shiftRepository = null;
            Shift existingShift = shiftRepository.GetById(ShiftPrimaryKey);

            if (existingShift == null)
            {
                this.log.Error("Data not exist in the Database!");
                return;
            }

            string shiftStateAsStringInteger = splitted.LastOrDefault();

            if (string.IsNullOrWhiteSpace(shiftStateAsStringInteger))
            {
                this.log.Error("Shift state cannot be whitespace or null!");
                return;
            }

            int shiftStateAsInteger = 0;

            if (!int.TryParse(shiftStateAsStringInteger, out shiftStateAsInteger))
            {
                this.log.Error("Shift state can only be integer value between 0 to 4!");
                return;
            }


            ShiftStateType shiftCurrentState = ShiftStateType.Finished;

            if (!Enum.TryParse<ShiftStateType>(shiftStateAsStringInteger, out shiftCurrentState))
            {
                this.log.Error("Shift state value must be between 0 to 4!");
                return;
            }

            existingShift.StateType = shiftCurrentState;

            dynamic unitOfwork = null;

            var erpOperationResult = erpClient.PushShiftState(ShiftPrimaryKey, shiftCurrentState);

            if (erpOperationResult)
            {
                unitOfwork.Commit();
            }
        }
    }
}
