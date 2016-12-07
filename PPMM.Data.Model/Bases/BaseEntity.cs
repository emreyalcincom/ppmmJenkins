using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Bases
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PPMM.Data.Model.Bases.IEntity" />
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
    }
}
