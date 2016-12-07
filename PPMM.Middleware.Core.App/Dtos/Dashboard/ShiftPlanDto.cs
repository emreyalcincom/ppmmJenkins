using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App.Dtos.Dashboard
{
    public class ShiftPlanDto
    {
        /// <summary>
        /// Gets or sets the shift identifier.
        /// </summary>
        /// <value>
        /// The shift identifier.
        /// </value>
        public int ShiftId { get; set; }

        /// <summary>
        /// Gets or sets the name of the shift.
        /// </summary>
        /// <value>
        /// The name of the shift.
        /// </value>
        public string ShiftName { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the operation identifier.
        /// </summary>
        /// <value>
        /// The operation identifier.
        /// </value>
        public int OperationId { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the remaining percentage.
        /// </summary>
        /// <value>
        /// The remaining percentage.
        /// </value>
        public double RemainingPercentage { get; set; }
    }
}
