using PPMM.Data.Model.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPMM.Data.Model.Entities
{
    public class Product: BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the genus.
        /// </summary>
        /// <value>
        /// The genus.
        /// </value>
        [ForeignKey("ProductGenusId")]
        public virtual ProductGenus ProductGenus { get; set; }

        /// <summary>
        /// Gets or sets the product genus identifier.
        /// </summary>
        /// <value>
        /// The product genus identifier.
        /// </value>
        public int ProductGenusId { get; set; }

        /// <summary>
        /// Gets or sets the stock code.
        /// </summary>
        /// <value>
        /// The stock code.
        /// </value>
        public string StockCode { get; set; }
    }
}
