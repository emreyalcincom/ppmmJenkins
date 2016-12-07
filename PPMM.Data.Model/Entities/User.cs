using PPMM.Data.Model.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPMM.Data.Model.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
    }
}
