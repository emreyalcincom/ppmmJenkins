using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class ProductGenus: BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGenus" /> class.
        /// </summary>
        public ProductGenus()
        {
            this.Products = new List<Product>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        [ForeignKey("ParentId")]
        public virtual ProductGenus Parent { get; set; }

        /// <summary>
        /// Gets or sets the child identifier.
        /// </summary>
        /// <value>
        /// The child identifier.
        /// </value>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Product> Products { get; set; }
    }
}
