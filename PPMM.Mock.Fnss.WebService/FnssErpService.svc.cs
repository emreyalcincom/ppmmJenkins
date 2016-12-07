using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PPMM.Data.Model.Enumerations;
using PPMM.Data.Model.Entities;
using PPMM.Data.Context;

namespace PPMM.Mock.Fnss.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class FnssErpService : IFnssErpService
    {
        /// <summary>
        /// Pushes the state of the shift.
        /// </summary>
        /// <param name="shiftId">The shift identifier.</param>
        /// <param name="stateType">Type of the state.</param>
        /// <returns></returns>
        public bool PushShiftState(string shiftId, ShiftStateType stateType)
        {
            return true;
        }

        /// <summary>
        /// Gets the waiting work orders.
        /// </summary>
        /// <returns></returns>
        public List<WorkOrder> GetWaitingWorkOrders()
        {
            using (PpmmContext context = new PpmmContext())
            {
                var waitingWorkOrders = context.WorkOrders.Where(x => x.StateType == WorkStateType.Waiting).ToList();

                return waitingWorkOrders;
            }
        }
    }
}
