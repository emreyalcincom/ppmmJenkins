using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PPMM.Middleware.Core.App
{
    /// <summary>
    /// 
    /// </summary>
    public class BatchOperator : IBatchOperator
    {
        /// <summary>
        /// The working timer
        /// </summary>
        private Timer workingTimer;

        /// <summary>
        /// The log
        /// </summary>
        private ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchOperator"/> class.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public BatchOperator(TimeSpan interval)
        {
            this.workingTimer = new Timer(interval.TotalMilliseconds);
            this.workingTimer.Elapsed += this.WorkingTimer_Elapsed;
            this.log = log4net.LogManager.GetLogger
   (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            this.log.Info("Batch operator initialized.");
        }

        /// <summary>
        /// Handles the Elapsed event of the WorkingTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void WorkingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.StartBatchOperations();
        }

        /// <summary>
        /// Starts the batch operations.
        /// </summary>
        private void StartBatchOperations()
        {

        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.workingTimer.Start();
            this.log.Info("Batch operator started.");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.workingTimer.Stop();
            this.log.Info("Batch operator stopped.");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.workingTimer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
