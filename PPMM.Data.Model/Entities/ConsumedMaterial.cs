using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class ConsumedMaterial: BaseEntity
    {
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>
        /// The material.
        /// </value>
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        /// <summary>
        /// Gets or sets the material identifier.
        /// </summary>
        /// <value>
        /// The material identifier.
        /// </value>
        public int MaterialId { get; set; }

        /// <summary>
        /// Gets or sets the type of the quantity.
        /// </summary>
        /// <value>
        /// The type of the quantity.
        /// </value>
        [ForeignKey("QuantityId")]
        public virtual Quantity QuantityType { get; set; }

        /// <summary>
        /// Gets or sets the quantity identifier.
        /// </summary>
        /// <value>
        /// The quantity identifier.
        /// </value>
        public int QuantityId { get; set; }

        /// <summary>
        /// Gets or sets the quantity value.
        /// </summary>
        /// <value>
        /// The quantity value.
        /// </value>
        public string QuantityValue { get; set; }
    }
}
