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
    /// <seealso cref="PPMM.Middleware.Core.App.Dtos.WorkOrders.IAssignWorkOrderResult" />
    public abstract class AssignWorkOrderResultBase : IAssignWorkOrderResult
    {
        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public WorkOrderResultType ResultType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignWorkOrderResult" /> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        public AssignWorkOrderResultBase(WorkOrderResultType resultType = WorkOrderResultType.Success)
        {
            this.ResultType = resultType;
        }
    }
}
