using PPMM.Data.Model.Bases;
using PPMM.Data.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class OperationStatus: BaseEntity
    {
        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        [ForeignKey("OperationId")]
        public virtual Operation Operation { get; set; }

        /// <summary>
        /// Gets or sets the depended operation.
        /// </summary>
        /// <value>
        /// The depended operation.
        /// </value>
        [ForeignKey("DependedOperationStatusId")]
        public virtual OperationStatus DependedOperationStatus { get; set; }

        /// <summary>
        /// Gets or sets the depended operation identifier.
        /// </summary>
        /// <value>
        /// The depended operation identifier.
        /// </value>
        public int? DependedOperationStatusId { get; set; }

        /// <summary>
        /// Gets or sets the operation identifier.
        /// </summary>
        /// <value>
        /// The operation identifier.
        /// </value>
        public int OperationId { get; set; }

        /// <summary>
        /// Gets or sets the type of the state.
        /// </summary>
        /// <value>
        /// The type of the state.
        /// </value>
        public WorkStateType StateType { get; set; }

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
        /// Gets or sets the work order.
        /// </summary>
        /// <value>
        /// The work order.
        /// </value>
        [ForeignKey("WorkOrderId")]
        public WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public int WorkOrderId { get; set; }
    }
}
