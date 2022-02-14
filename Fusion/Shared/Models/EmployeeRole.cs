using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class EmployeeRole
    {
        public int EmployeeID { get; set; }

        public int RoleID { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? AddedByID { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedByID { get; set; }
    }
}
