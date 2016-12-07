using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class ProductionStageResourceDefinitionOrder: BaseEntity
    {
        /// <summary>
        /// Gets or sets the consumed.
        /// </summary>
        /// <value>
        /// The consumed.
        /// </value>
        public virtual ResourceDefinition ResourceDefinition { get; set; }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        /// <value>
        /// The stage.
        /// </value>
        public virtual Operation Stage { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }
    }
}
