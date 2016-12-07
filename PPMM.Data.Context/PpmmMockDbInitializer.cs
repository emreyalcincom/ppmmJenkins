using PPMM.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Context
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DropCreateDatabaseAlways{PPMM.Data.Context.PpmmContext}" />
    public class PpmmMockDbInitializer : DropCreateDatabaseAlways<PpmmContext>
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(PpmmContext context)
        {
            #region InitializeShifts

            Shift waterJetShift = new Shift
            {
                Name = "Water Jet Shift",
                Code = "B01"
            };

            Shift uniponyShift = new Shift
            {
                Name = "Unipony Shift",
                Code = "B02"
            };

            Shift radilDrillBredaShift = new Shift
            {
                Name = "Radial Drill Breda Shift",
                Code = "B03"
            };

            Shift machiningInspectionShift = new Shift
            {
                Name = "Machining Inspection Shift",
                Code = "B04"
            };

            Shift sawDustShift = new Shift
            {
                Name = "Sawdust Shift",
                Code = "B05"
            };

            Shift metalJoineryShift = new Shift
            {
                Name = "Metal Joinery Shift",
                Code = "B06"
            };

            context.Shifts.Add(waterJetShift);
            context.Shifts.Add(uniponyShift);
            context.Shifts.Add(radilDrillBredaShift);
            context.Shifts.Add(machiningInspectionShift);
            context.Shifts.Add(sawDustShift);
            context.Shifts.Add(metalJoineryShift);

            #endregion

            #region InitializeProductGenuses

            ProductGenus missileProductType = new ProductGenus
            {
                Name = "Missile"
            };

            ProductGenus radarProductType = new ProductGenus
            {
                Name = "Radar"
            };

            ProductGenus weaponProductType = new ProductGenus
            {
                Name = "Weapon"
            };

            ProductGenus attachableWeaponProductType = new ProductGenus
            {
                Name = "Attachable Weapon",
                Parent = weaponProductType
            };

            context.ProductGenus.Add(missileProductType);
            context.ProductGenus.Add(radarProductType);
            context.ProductGenus.Add(weaponProductType);
            context.ProductGenus.Add(attachableWeaponProductType);

            #endregion

            #region InitializeProducts

            Product akashProduct = new Product
            {
                Name = "Akash",
                ProductGenus = missileProductType,
                StockCode = "ABC1230"
            };

            Product asterProduct = new Product
            {
                Name = "Aster",
                ProductGenus = missileProductType,
                StockCode = "ABC1231"
            };

            Product mim3nikeAjaxProduct = new Product
            {
                Name = "MIM-3 Nike Ajax",
                ProductGenus = missileProductType,
                StockCode = "ABC1232"
            };

            Product rim2terrierProduct = new Product
            {
                Name = "RIM-2 Terrier",
                ProductGenus = missileProductType,
                StockCode = "ABC1233"
            };

            Product sa1guildProduct = new Product
            {
                Name = "SA-1 Guild",
                ProductGenus = missileProductType,
                StockCode = "ABC1234"
            };

            Product rolandProduct = new Product
            {
                Name = "MIM-115 Roland",
                ProductGenus = missileProductType,
                StockCode = "ABC1235"
            };

            Product seaWolfProduct = new Product
            {
                Name = "Sea Wolf",
                ProductGenus = missileProductType,
                StockCode = "ABC1236"
            };

            context.Products.Add(akashProduct);
            context.Products.Add(asterProduct);
            context.Products.Add(mim3nikeAjaxProduct);
            context.Products.Add(rim2terrierProduct);
            context.Products.Add(sa1guildProduct);
            context.Products.Add(rolandProduct);
            context.Products.Add(seaWolfProduct);

            #endregion

            #region InitializeResources

            #endregion

            #region InitializeOperations

            Operation waterJetOperation = new Operation
            {
                Definition = "WATER JET (FLOW)",
                ProcessSpan = new TimeSpan(2, 15, 0),
                Shift = waterJetShift
            };

            Operation uniponyMillOperation = new Operation
            {
                Definition = "UNIPONY MILL",
                ProcessSpan = new TimeSpan(1, 0, 0),
                Shift = uniponyShift
            };

            Operation radialDrillBredaOperation = new Operation
            {
                Definition = "RADIAL DRILL BREDA I (BIG)",
                ProcessSpan = new TimeSpan(1, 30, 0),
                Shift = radilDrillBredaShift
            };

            Operation machiningInspectionOperation = new Operation
            {
                Definition = "MACHINING INSPECTION",
                ProcessSpan = new TimeSpan(3, 0, 0),
                Shift = machiningInspectionShift
            };

            context.Operations.Add(waterJetOperation);
            context.Operations.Add(uniponyMillOperation);
            context.Operations.Add(radialDrillBredaOperation);
            context.Operations.Add(machiningInspectionOperation);

            #endregion

            #region InitializeWorks

            Work keepingPlateWork = new Work
            {
                Note = "",
                Product = mim3nikeAjaxProduct,
                Title = "PLATE, CONVERSATIVE ATALETSEL NAVIGATION SYSTEM",
                Operations = new List<Operation>() { waterJetOperation, radialDrillBredaOperation, uniponyMillOperation, machiningInspectionOperation }
            };

            context.Works.Add(keepingPlateWork);

            #endregion

            #region InitializeWorkOrders

            WorkOrder keepingPlateWorkOrder = new WorkOrder
            {
                DeliveryDate = DateTime.Now.AddDays(67),
                Number = "123456",
                Quantity = 1,
                StateType = Model.Enumerations.WorkStateType.Waiting,
                Work = keepingPlateWork
            };

            context.WorkOrders.Add(keepingPlateWorkOrder);

            #endregion

            #region InitializeWorkOrders

            OperationOrder keepingPlateWorkOrder1 = new OperationOrder
            {
                Operation = waterJetOperation,
                Work = keepingPlateWork,
                Order = 1
            };

            OperationOrder keepingPlateWorkOrder2 = new OperationOrder
            {
                Operation = uniponyMillOperation,
                Work = keepingPlateWork,
                Order = 2
            };

            OperationOrder keepingPlateWorkOrder3 = new OperationOrder
            {
                Operation = radialDrillBredaOperation,
                Work = keepingPlateWork,
                Order = 3
            };

            OperationOrder keepingPlateWorkOrder4 = new OperationOrder
            {
                Operation = machiningInspectionOperation,
                Work = keepingPlateWork,
                Order = 4
            };

            context.OperationOrders.Add(keepingPlateWorkOrder1);
            context.OperationOrders.Add(keepingPlateWorkOrder2);
            context.OperationOrders.Add(keepingPlateWorkOrder3);
            context.OperationOrders.Add(keepingPlateWorkOrder4);

            #endregion

            base.Seed(context);
        }
    }
}
