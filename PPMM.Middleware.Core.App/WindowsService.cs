using log4net;
using System;
using System.ServiceProcess;

namespace PPMM.Middleware.Core.App
{
    partial class WindowsService : ServiceBase
    {
        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsService"/> class.
        /// </summary>
        public WindowsService()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
            this.log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            MiddlewareInstance intance = new MiddlewareInstance();
            intance.StartWorking();

            this.log.Info("Windows service started.");
        }


        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            this.log.Info("Windows service stopped.");
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.log.Fatal("Windows service stopped. Unhandled exception occured." + " Exception: <<" + e.ExceptionObject.ToString() + ">> " + "Please look to event viewer for more detail.");

            if (e.IsTerminating)
            {
                this.OnStop();
            }
        }
    }
}
