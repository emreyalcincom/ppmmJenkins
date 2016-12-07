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
    public class WorkOrder: BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkOrder"/> class.
        /// </summary>
        public WorkOrder()
        {
            this.OperationStatuses = new List<OperationStatus>();
        }

        /// <summary>
        /// Gets or sets the work.
        /// </summary>
        /// <value>
        /// The work.
        /// </value>
        [ForeignKey("WorkId")]
        public virtual Work Work { get; set; }

        /// <summary>
        /// Gets or sets the work identifier.
        /// </summary>
        /// <value>
        /// The work identifier.
        /// </value>
        public int WorkId { get; set; }

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
        /// Gets or sets the operation statuses.
        /// </summary>
        /// <value>
        /// The operation statuses.
        /// </value>
        public virtual ICollection<OperationStatus> OperationStatuses { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>
        /// The delivery date.
        /// </value>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the work oder no.
        /// </summary>
        /// <value>
        /// The work oder no.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the type of the state.
        /// </summary>
        /// <value>
        /// The type of the state.
        /// </value>
        public WorkStateType StateType { get; set; }
    }
}
