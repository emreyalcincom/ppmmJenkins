using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class Production: BaseEntity
    {
        /// <summary>
        /// Gets or sets the stage orders.
        /// </summary>
        /// <value>
        /// The stage orders.
        /// </value>
        public virtual ICollection<OperationOrder> StageOrders { get; set; }

        /// <summary>
        /// Gets or sets the manufactured products.
        /// </summary>
        /// <value>
        /// The manufactured products.
        /// </value>
        public virtual ICollection<Product> ManufacturedProducts { get; set; }
    }
}
