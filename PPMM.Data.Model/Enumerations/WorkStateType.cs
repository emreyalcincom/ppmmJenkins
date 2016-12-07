using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Enumerations
{
    public enum WorkStateType
    {
        /// <summary>
        /// The in work
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// The finished
        /// </summary>
        Finished = 1,

        /// <summary>
        /// The waiting
        /// </summary>
        InWork = 2,

        /// <summary>
        /// The system stop
        /// </summary>
        SystemStop = 3
    }
}
