using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class OperationOrder: BaseEntity
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
        /// Gets or sets the operation identifier.
        /// </summary>
        /// <value>
        /// The operation identifier.
        /// </value>
        public int OperationId { get; set; }

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
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }
    }
}
