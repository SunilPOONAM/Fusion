using Microsoft.AspNetCore.Mvc;
using Fusion.Server.Service;
using Fusion.Server.Service.imp;

using Fusion.Shared.Models;
using System.Collections.Generic;


namespace Fusion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        AssignmentBase db = new ManageAssignments();
        // GET: api/<OrderLstController>
        [HttpGet]
        public ResponseModel GetAssignments(string empID)
        {
            ResponseModel res = new ResponseModel();
            List<vwAssignment> lst = new List<vwAssignment>();
            lst = db.GetAssignments(empID);
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }
        
        public ResponseModel GetTeamAssignments(string empID)
        {
            ResponseModel res = new ResponseModel();
            List<vwAssignment> lst = new List<vwAssignment>();
            lst = db.GetTeamAssignments(empID);
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }
        
    }
}
