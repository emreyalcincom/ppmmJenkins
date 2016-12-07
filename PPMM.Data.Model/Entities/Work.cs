using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class Work: BaseEntity
    {
        /// <summary>
        /// Gets or sets the operation statuses.
        /// </summary>
        /// <value>
        /// The operation statuses.
        /// </value>
        public virtual ICollection<Operation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the work orders.
        /// </summary>
        /// <value>
        /// The work orders.
        /// </value>
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        /// <summary>
        /// Gets or sets the operation orders.
        /// </summary>
        /// <value>
        /// The operation orders.
        /// </value>
        public virtual ICollection<OperationOrder> OperationOrders { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }
    }
}
