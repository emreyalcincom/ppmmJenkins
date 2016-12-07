using PPMM.Data.Model.Entities;
using PPMM.Data.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPMM.Mock.Fnss.WebService
{
    [ServiceContract]
    public interface IFnssErpService
    {
        /// <summary>
        /// Pushes the state of the shift.
        /// </summary>
        /// <param name="shiftId">The shift identifier.</param>
        /// <param name="stateType">Type of the state.</param>
        /// <returns></returns>
        [OperationContract]
        bool PushShiftState(string shiftId, ShiftStateType stateType);

        [OperationContract]
        List<WorkOrder> GetWaitingWorkOrders();
    }

}
