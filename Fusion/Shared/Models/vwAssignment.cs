using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public partial class vwAssignment
    {
        public int AssignID { get; set; }
        public int ObjectID { get; set; }
        public string StatusCode { get; set; }
        public string CodeDesc { get; set; }
        public int LeaderID { get; set; }
        public int EmployeeID { get; set; }
        public string Assignee { get; set; }
        public string AssignedObject { get; set; }
        public string AssignedObjectType { get; set; }
        public string CustomerName { get; set; }
        public string Nickname { get; set; }
        public string Role { get; set; }
        public string Responsibility { get; set; }
        public bool IsPrimary { get; set; }
        public Nullable<System.DateTime> FollowUp { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> NoOfDaysFromCreation { get; set; }
    }
}
