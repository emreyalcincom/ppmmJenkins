using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Context.Conventions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.Conventions.Convention" />
    public class DateTime2Convention : Convention
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTime2Convention"/> class.
        /// </summary>
        public DateTime2Convention()
        {
            this.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
