using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class ProduceAmount: BaseEntity
    {
        /// <summary>
        /// Gets or sets the production.
        /// </summary>
        /// <value>
        /// The production.
        /// </value>
        [ForeignKey("ProductionId")]
        public virtual Production Production { get; set; }

        /// <summary>
        /// Gets or sets the production identifier.
        /// </summary>
        /// <value>
        /// The production identifier.
        /// </value>
        public int ProductionId { get; set; }

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

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public int Amount { get; set; }
    }
}
