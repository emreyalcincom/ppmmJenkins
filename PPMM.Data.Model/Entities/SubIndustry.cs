using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class SubIndustry: BaseEntity
    {
        /// <summary>
       /// Gets or sets the manufactured products.
       /// </summary>
       /// <value>
       /// The manufactured products.
       /// </value>
        public virtual ICollection<Product> ManufacturedProducts { get; set; }
    }
}
