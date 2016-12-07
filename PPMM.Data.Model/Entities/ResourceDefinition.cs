using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
  public  class ResourceDefinition: BaseEntity
    {
        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>
        /// The type of the resource.
        /// </value>
        [ForeignKey("ResourceId")]
        public virtual Resource ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public int ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the resource value.
        /// </summary>
        /// <value>
        /// The resource value.
        /// </value>
        public string ResourceValue { get; set; }

        /// <summary>
        /// Gets or sets the production capacity.
        /// </summary>
        /// <value>
        /// The production capacity.
        /// </value>
        public double ProductionCapacity { get; set; }
    }
}
