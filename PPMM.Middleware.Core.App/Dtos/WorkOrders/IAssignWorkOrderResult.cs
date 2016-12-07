using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Middleware.Core.App.Dtos.WorkOrders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAssignWorkOrderResult
    {
        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        WorkOrderResultType ResultType { get;  }
    }
}
