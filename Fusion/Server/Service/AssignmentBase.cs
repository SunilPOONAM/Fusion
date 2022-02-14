using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class AssignmentBase 
    {
        public abstract List<vwAssignment> GetAssignments(string empID);
        public abstract List<vwAssignment> GetTeamAssignments(string empID);
        public abstract List<vwAssignment> GetAssignmentsBytblName(string id, string tblname);
        public abstract bool AddAssignment(Assignment asgn);
        public abstract bool UpdateAssignment(Assignment asgn);
    }
}