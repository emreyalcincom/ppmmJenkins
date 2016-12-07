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
    /// <seealso cref="PPMM.Middleware.Core.App.Dtos.WorkOrders.AssignWorkOrderResultBase" />
    public class AssignWorkOrderNeedDeliveryUpdate : AssignWorkOrderResultBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignWorkOrderNeedDeliveryUpdate" /> class.
        /// </summary>
        public AssignWorkOrderNeedDeliveryUpdate() : base(WorkOrderResultType.NeedDeliveryDateUpdate)
        {
            this.DeliveryUpdates = new List<DeliveryUpdateDto>();
        }

        /// <summary>
        /// Gets or sets the delivery updates.
        /// </summary>
        /// <value>
        /// The delivery updates.
        /// </value>
        public List<DeliveryUpdateDto> DeliveryUpdates { get; set; }

        /// <summary>
        /// Gets or sets the recomended date.
        /// </summary>
        /// <value>
        /// The recomended date.
        /// </value>
        public DateTime RecomendedDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the current delivery date.
        /// </summary>
        /// <value>
        /// The current delivery date.
        /// </value>
        public DateTime CurrentDeliveryDate { get; set; }
    }
}
