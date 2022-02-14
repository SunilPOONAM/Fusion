using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Assignment
    {
        public int AssignID { get; set; }
        public string AssignedObjectType { get; set; }
        public int ObjectID { get; set; }
        public int EmployeeID { get; set; }
        public Nullable<System.DateTime> FollowUp { get; set; }
        public string Responsibility { get; set; }
        public bool IsPrimary { get; set; }
        public string Role { get; set; }
        public string StatusCode { get; set; }
    }
}
