using PPMM.Data.Context.Conventions;
using PPMM.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Context
{
    public class PpmmContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PpmmContext" /> class.
        /// </summary>
        /// <param name="useMockInitializer">if set to <c>true</c> [use mock initializer].</param>
        public PpmmContext(bool useMockInitializer = false) : base("name=ppmmCon")
        {
            Configuration.ProxyCreationEnabled = false;

            if (useMockInitializer)
            {
                Database.SetInitializer(new PpmmMockDbInitializer());
            }
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DateTime2Convention());

            modelBuilder.Entity<Operation>()
                   .HasRequired(x => x.Work)
                   .WithMany(x => x.Operations)
                   .HasForeignKey(x => x.WorkId)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Operation>()
                 .HasOptional(x => x.Resource);

            modelBuilder.Entity<Product>()
               .HasRequired(x => x.ProductGenus)
               .WithMany(x=>x.Products)
               .HasForeignKey(x=>x.ProductGenusId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductGenus>()
           .HasOptional(x => x.Parent);

            modelBuilder.Entity<Work>()
       .HasRequired(x => x.Product)
       .WithMany()
       .HasForeignKey(x=>x.ProductId)
       .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
  .HasRequired(x => x.Work)
  .WithMany(x=>x.WorkOrders)
  .HasForeignKey(x=>x.WorkId)
  .WillCascadeOnDelete(false);

            modelBuilder.Entity<OperationStatus>()
.HasRequired(x => x.WorkOrder)
.WithMany(x=>x.OperationStatuses)
.HasForeignKey(x=>x.WorkOrderId)
.WillCascadeOnDelete(false);

            modelBuilder.Entity<OperationStatus>()
.HasRequired(x => x.Operation)
.WithMany()
.HasForeignKey(x=>x.OperationId)
.WillCascadeOnDelete(false);

            modelBuilder.Entity<OperationOrder>()
.HasRequired(x => x.Operation)
.WithMany(x=>x.OperationOrders)
.HasForeignKey(x=>x.OperationId)
.WillCascadeOnDelete(false);

            modelBuilder.Entity<OperationOrder>()
.HasRequired(x => x.Work)
.WithMany(x=>x.OperationOrders)
.HasForeignKey(x=>x.WorkId)
.WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>().ToTable(typeof(Work).Name);
            modelBuilder.Entity<WorkOrder>().ToTable(typeof(WorkOrder).Name);
            modelBuilder.Entity<Operation>().ToTable(typeof(Operation).Name);
            modelBuilder.Entity<OperationOrder>().ToTable(typeof(OperationOrder).Name);
            modelBuilder.Entity<OperationStatus>().ToTable(typeof(OperationStatus).Name);
            modelBuilder.Entity<Shift>().ToTable(typeof(Shift).Name);
            modelBuilder.Entity<Product>().ToTable(typeof(Product).Name);
            modelBuilder.Entity<ProductGenus>().ToTable(typeof(ProductGenus).Name);
            modelBuilder.Entity<Quantity>().ToTable(typeof(Quantity).Name);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the quantities.
        /// </summary>
        /// <value>
        /// The quantities.
        /// </value>
        public DbSet<Quantity> Quantities { get; set; }

        /// <summary>
        /// Gets or sets the works.
        /// </summary>
        /// <value>
        /// The works.
        /// </value>
        public DbSet<Work> Works { get; set; }

        /// <summary>
        /// Gets or sets the work orders.
        /// </summary>
        /// <value>
        /// The work orders.
        /// </value>
        public DbSet<WorkOrder> WorkOrders { get; set; }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>
        /// The operations.
        /// </value>
        public DbSet<Operation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the operation orders.
        /// </summary>
        /// <value>
        /// The operation orders.
        /// </value>
        public DbSet<OperationOrder> OperationOrders { get; set; }

        /// <summary>
        /// Gets or sets the operation statuses.
        /// </summary>
        /// <value>
        /// The operation statuses.
        /// </value>
        public DbSet<OperationStatus> OperationStatuses { get; set; }

        /// <summary>
        /// Gets or sets the shifts.
        /// </summary>
        /// <value>
        /// The shifts.
        /// </value>
        public DbSet<Shift> Shifts { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the product genus.
        /// </summary>
        /// <value>
        /// The product genus.
        /// </value>
        public DbSet<ProductGenus> ProductGenus { get; set; }
    }
}
