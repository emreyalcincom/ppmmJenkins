using PPMM.Data.Model.Bases;
using PPMM.Data.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class Shift: BaseEntity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the type of the state.
        /// </summary>
        /// <value>
        /// The type of the state.
        /// </value>
        public ShiftStateType StateType { get; set; }
    }
}
