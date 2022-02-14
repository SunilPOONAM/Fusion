using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Discussion
    {
        public int DiscussionID { get; set; }
        public string AssignedObjectType { get; set; }
        public int ObjectID { get; set; }
        public string Contact { get; set; }
        public string Summary { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        
    }
}
