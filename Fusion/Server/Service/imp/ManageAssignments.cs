using Fusion.Server.Helper;
using Fusion.Shared;
using Fusion.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Fusion.Server.Service.imp
{
    public class ManageAssignments : AssignmentBase
    {
        #region Declaration
        private readonly SqlDataAccess _db = new SqlDataAccess();
        DataTable dtContainer;
        #endregion

        public override List<vwAssignment> GetAssignments(string empID)
        {
            List<vwAssignment> retval = new List<vwAssignment>();
            dtContainer = new DataTable();
            try
            {
                string query = "select * from vwAssignments where EmployeeID=" + empID + " and (StatusCode Is Null OR StatusCode not in (5, 6))";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<vwAssignment>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override List<vwAssignment> GetTeamAssignments(string empID)
        {
            List<vwAssignment> retval = new List<vwAssignment>();
            dtContainer = new DataTable();
            try
            {
                string query = "select * from vwAssignedToTeam where LeaderID=" + empID + " and (StatusCode Is Null OR StatusCode not in (5, 6))";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<vwAssignment>(dtContainer);

            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override List<vwAssignment> GetAssignmentsBytblName(string id, string tblname)
        {
            List<vwAssignment> retval = new List<vwAssignment>();
            dtContainer = new DataTable();
            string query = "";
            try
            {                
                query = "select * from vwAssignments where ObjectID=" + id + " and AssignedObjectType like '%" + tblname + "%'";
                dtContainer = _db.DataTable_return(query);
                var res = GenerateSQL.ConvertToList<vwAssignment>(dtContainer);
                retval = res.Where(c => c.StatusCode != "5" && c.StatusCode != "6").ToList();
            }
            catch (Exception ex)
            {

            }
            return retval;
        }

        public override bool AddAssignment(Assignment asgn)
        {
            bool retval = false;
            int result = 0;

            string followupdate = asgn.FollowUp != null ? Convert.ToDateTime(asgn.FollowUp).ToString("MM-dd-yyyy") : "";

            try
            {
                asgn.SetPropertyValues();
                string query = "insert into Assignments(EmployeeID,IsPrimary,ObjectID,Responsibility,Role,StatusCode,AssignedObjectType,FollowUp) values (" + asgn.EmployeeID + ",'" + asgn.IsPrimary + "'," + asgn.ObjectID + ",'" + asgn.Responsibility + "','" + asgn.Role + "','" + asgn.StatusCode + "','" + asgn.AssignedObjectType + "','" + followupdate + "')";
                result = _db.ExecuteNonQuery_IUD(query);
                if (result == 1)
                {
                    retval = true;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }

        public override bool UpdateAssignment(Assignment asgn)
        {
            bool retval = false;
            int result = 0;

            string followupdate = asgn.FollowUp != null ? Convert.ToDateTime(asgn.FollowUp).ToString("MM-dd-yyyy") : "";

            try
            {
                asgn.SetPropertyValues();
                string query = "UPDATE Assignments SET AssignedObjectType ='" + asgn.AssignedObjectType + "',ObjectID =" + asgn.ObjectID + ",EmployeeID =" + asgn.EmployeeID + ",FollowUp ='" + followupdate + "',Responsibility ='" + asgn.Responsibility + "',IsPrimary ='" + asgn.IsPrimary + "'," +
                               " Role = '" + asgn.Role + "', StatusCode = '" + asgn.StatusCode + "' where AssignID=" + asgn.AssignID;
                result = _db.ExecuteNonQuery_IUD(query);
                if (result == 1)
                {
                    retval = true;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }
    }
}