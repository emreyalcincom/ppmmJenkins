using PPMM.Data.Context;
using PPMM.Data.Model.Entities;
using PPMM.Data.Model.Enumerations;
using PPMM.Middleware.Core.App.Dtos.Dashboard;
using PPMM.Middleware.Core.App.Dtos.WorkOrders;
using PPMM.Middleware.Core.App.FNSSErpService;
using PPMM.Middleware.Core.App.Helpers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PPMM.Middleware.Core.App.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BusinessController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessController"/> class.
        /// </summary>
        public BusinessController()
        {
        }
        
        /// <summary>
        /// Assigns the work.
        /// </summary>
        /// <param name="assigningWorkOrderId">The assigning work order identifier.</param>
        [HttpGet]
        [Route("AssignWork")]
        public HttpResponseMessage AssignWork(int assigningWorkOrderId)
        {

            var client = new FnssErpServiceClient();
            var waitingWorkOrders = client.GetWaitingWorkOrders();

            using (PpmmContext context = new PpmmContext())
            {
                WorkOrder neededWorkOrder = context.WorkOrders
                    .Include("Work")
                    .Include("Work.Operations")
                    .Include("Work.Operations.Shift")
                    .Include("Work.OperationOrders")
                    .FirstOrDefault(
                    x => x.Id == assigningWorkOrderId && x.StateType == WorkStateType.Waiting);

                if (neededWorkOrder != null)
                {
                    //Query the the using shifts in the workorder.
                    List<Shift> usingShiftsInWorkOrder = neededWorkOrder.Work.Operations.Select(x => x.Shift).ToList();

                    //Get the working orders of the shifts in the workorder.
                    var shiftsWorkingOrders = context.OperationOrders.Where(x => x.WorkId == neededWorkOrder.WorkId);

                    //Order them by order field.
                    var orderedShiftsWorkingOrders = neededWorkOrder.Work.OperationOrders.OrderBy(x => x.Order);

                    var starterShiftForWorkOrder = orderedShiftsWorkingOrders.FirstOrDefault();
                    var operationIds = orderedShiftsWorkingOrders.Select(y => y.OperationId).ToList();

                    var operationStatusesForShifts = context.OperationStatuses
                        .Include("Operation")
                        .Include("Operation.Shift")
                        .Where(x => operationIds.Contains(x.OperationId) &&
                        x.StateType == WorkStateType.InWork);

                    List<OperationStatus> creatingOperationStatuses = new List<OperationStatus>();

                    TimeSpan totalProcessingSpan = new TimeSpan();

                    neededWorkOrder.Work.Operations.ToList().ForEach(x => totalProcessingSpan += x.ProcessSpan);

                    //Is Starter shift InWork ?
                    if (operationStatusesForShifts.ToList().Any(x => x.Operation.ShiftId == starterShiftForWorkOrder.Operation.ShiftId))
                    {

                    }

                    //Prepare algorithm result for service response
                    WorkOrderResultType workOrderResult = WorkOrderResultType.Success;
                    AssignWorkOrderNeedDeliveryUpdate workOrderDeliveryUpdate = null;

                    //Needed shifts is in workorder thats are currently InWork states.
                    if (operationStatusesForShifts.Any())
                    {
                        foreach (var everyOperationStatusForShift in operationStatusesForShifts)
                        {
                            TimeSpan expectedSpan = everyOperationStatusForShift.Operation.ProcessSpan - (DateTime.Now - everyOperationStatusForShift.StartTime);
                            DateTime expectedDateTime = DateTime.Now.Add(expectedSpan);

                            //Assigned the operation to the shift.
                            if (expectedDateTime <= neededWorkOrder.DeliveryDate)
                            {
                                OperationStatus creatingOperationStatus = new OperationStatus
                                {
                                    StartTime = DateTime.Now,
                                    OperationId = everyOperationStatusForShift.OperationId,
                                    WorkOrderId = neededWorkOrder.Id,
                                    EndTime = DateTime.Now.Add(everyOperationStatusForShift.Operation.ProcessSpan)
                                };

                                context.OperationStatuses.Attach(creatingOperationStatus);
                            }
                            else
                            {
                                if (workOrderResult != WorkOrderResultType.NeedDeliveryDateUpdate)
                                {
                                    workOrderResult = WorkOrderResultType.NeedDeliveryDateUpdate;
                                    workOrderDeliveryUpdate = new AssignWorkOrderNeedDeliveryUpdate();
                                    workOrderDeliveryUpdate.CurrentDeliveryDate = neededWorkOrder.DeliveryDate;

                                }

                                workOrderDeliveryUpdate.DeliveryUpdates.Add(new DeliveryUpdateDto
                                {
                                    OperationId = everyOperationStatusForShift.OperationId,
                                    OperationName = everyOperationStatusForShift.Operation.Definition,
                                    NeededMinimumDate = expectedDateTime,
                                    ShiftId = everyOperationStatusForShift.Operation.ShiftId,
                                    ShiftName = everyOperationStatusForShift.Operation.Shift.Name
                                });
                            }
                        }
                    }
                    else
                    {
                        foreach (var freeOperation in neededWorkOrder.Work.Operations)
                        {
                            OperationStatus creatingOperationStatus = new OperationStatus
                            {
                                StartTime = DateTime.Now,
                                OperationId = freeOperation.Id,
                                WorkOrderId = neededWorkOrder.Id,
                                EndTime = DateTime.Now.Add(freeOperation.ProcessSpan),
                                StateType = WorkStateType.InWork
                            };

                            context.OperationStatuses.Add(creatingOperationStatus);
                        }

                        neededWorkOrder.StateType = WorkStateType.InWork;
                    }

                    context.SaveChanges();

                    switch (workOrderResult)
                    {
                        case WorkOrderResultType.Success:

                            return Request.CreateResponse(HttpStatusCode.OK, new ServiceResult<bool>(true, "WorkOrder assigned successfully"));

                        case WorkOrderResultType.NeedDeliveryDateUpdate:
                            return Request.CreateResponse(HttpStatusCode.OK, new ServiceResult<AssignWorkOrderNeedDeliveryUpdate>(workOrderDeliveryUpdate, "WorkOrder assigned with some criticals."));
                        default:
                            return Request.CreateResponse(HttpStatusCode.BadRequest, new ServiceResult<bool>("Not defined enum type: WorkOrderResultType!"));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ServiceResult<bool>("The workorder not exist or not in waiting order state."));
                }
            }

            //DateTime DeliveryDate = new DateTime(); // get it from work order.

            //DateTime expectedFinishDate = new DateTime(); //get it from recorded data from DB

            //TimeSpan processSpan = new TimeSpan();

            //dynamic workOrderRepository = null;
            //WorkOrder neededWorkOrder = workOrderRepository.GetById(assigningWorkOrderId);

            //var operatingShifts = neededWorkOrder.Work.Operations;

            //dynamic operationOrderRepository = null;
            //var operationOrders = operationOrderRepository.Find(x => x.Work.Id == neededWorkOrder.Work.Id);
            //var orderedShift = operationOrders.OrderBy(x => x.Order).First();

            //if (orderedShift.Shift.Status == ShiftStateType.Off)
            //{
            //    //Assign Work Order
            //}
            //else
            //{
            //    var currentlyWorkingOrder = workOrderRepository.First(x => x.OperationStatuses.Where(y => y.Operation.Shift.Id == orderedShift.Id && y.StateType == WorkStateType.InWork)); // orderedShift e ait workOrderları bulacam.

            //    var neededOperation = currentlyWorkingOrder.Work.Operations.First(x => x.Shift.Id == orderedShift.Id);

            //    var expectedFinishDate = DateTime.Now - neededOperation.StartTime
            //    if (neededOperation.ProcessSpan +
            //}


            //foreach (var orderedShift in orderedShifts)
            //{
            //    if (orderedShift.Operation.Shift ==)
            //    {

            //    }
            //}

            //if (neededWorkOrder.StateType == ShiftStateType.Off)
            //{
            //    //assign work
            //}
            //else
            //{
            //    if (neededWorkOrder.Production.)
            //    {

            //    }
            //}

        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("InitDb")]
        public HttpResponseMessage InitializeDatabase()
        {
            var log = log4net.LogManager.GetLogger
  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            using (PpmmContext context = new PpmmContext(true))
            {
                log.Info("Database initialized.");

                Quantity quantityPiece = new Quantity
                {
                    Name = "Piece"
                };

                context.Quantities.Add(quantityPiece);

                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ServiceResult<bool>(true, "Database initialized."));
        }

        /// <summary>
        /// Shiftses the plan.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ShiftPlans")]
        public HttpResponseMessage ShiftsPlan()
        {
            using (PpmmContext context = new PpmmContext())
            {
                var currentOperationStatuses = context.OperationStatuses.Include("Operation").Include("Operation.Shift").Include("WorkOrder").Where(x => x.StateType == WorkStateType.InWork);

                List<ShiftPlanDto> plans = new List<ShiftPlanDto>();

                foreach (var currentOperation in currentOperationStatuses)
                {
                    double remainingPercentage = ((currentOperation.EndTime - DateTime.Now).TotalDays / (currentOperation.EndTime - currentOperation.StartTime).TotalDays) * 100;

                    plans.Add(new ShiftPlanDto
                    {
                        EndTime = currentOperation.EndTime,
                        OperationId = currentOperation.OperationId,
                        OperationName = currentOperation.Operation.Definition,
                        RemainingPercentage = remainingPercentage,
                        ShiftId = currentOperation.Operation.ShiftId,
                        ShiftName = currentOperation.Operation.Shift.Name,
                        StartTime = currentOperation.StartTime
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new ServiceResult<List<ShiftPlanDto>>(plans, "plans returned successfully."));
            }
        }
    }
}
