using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class Operation: BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        public Operation()
        {
            this.Consumed = new List<ConsumedMaterial>();
            this.OperationOrders = new List<OperationOrder>();
        }

        /// <summary>
        /// Gets or sets the consumed.
        /// </summary>
        /// <value>
        /// The consumed.
        /// </value>
        public virtual ICollection<ConsumedMaterial> Consumed { get; set; }

        /// <summary>
        /// Gets or sets the operation orders.
        /// </summary>
        /// <value>
        /// The operation orders.
        /// </value>
        public virtual ICollection<OperationOrder> OperationOrders { get; set; }

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        [ForeignKey("ResourceId")]
        public virtual Resource Resource { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public int? ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the shift.
        /// </summary>
        /// <value>
        /// The shift.
        /// </value>
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }

        /// <summary>
        /// Gets or sets the shift identifier.
        /// </summary>
        /// <value>
        /// The shift identifier.
        /// </value>
        public int ShiftId { get; set; }

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
        /// Gets or sets the process span.
        /// </summary>
        /// <value>
        /// The process span.
        /// </value>
        public TimeSpan ProcessSpan { get; set; }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>
        /// The definition.
        /// </value>
        public string Definition { get; set; }
    }
}
