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
    public enum WorkOrderResultType
    {
        /// <summary>
        /// The success
        /// </summary>
        Success = 0,

        /// <summary>
        /// The need delivery date update
        /// </summary>
        NeedDeliveryDateUpdate = 1,
    }
}
