using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class DailyShift: BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyShift"/> class.
        /// </summary>
        public DailyShift()
        {
            this.Works = new List<Production>();
        }

        /// <summary>
        /// Gets or sets the type of the shift.
        /// </summary>
        /// <value>
        /// The type of the shift.
        /// </value>
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }

        /// <summary>
        /// Gets or sets the shift identifier.
        /// </summary>
        /// <value>
        /// The shift identifier.
        /// </value>
        public int ShiftId { get; set; }

        /// <summary>
        /// Gets the worker.
        /// </summary>
        /// <value>
        /// The worker.
        /// </value>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the work.
        /// </summary>
        /// <value>
        /// The work.
        /// </value>
        public virtual ICollection<Production> Works { get; set; }
    }
}
