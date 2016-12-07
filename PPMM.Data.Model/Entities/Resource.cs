using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class Resource: BaseEntity
    {
        public Resource()
        {
            this.ResourceDefinitions = new List<ResourceDefinition>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource definitions.
        /// </summary>
        /// <value>
        /// The resource definitions.
        /// </value>
        public virtual ICollection<ResourceDefinition> ResourceDefinitions { get; set; }
    }
}
