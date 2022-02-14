using Fusion.Server.Helper;
using Fusion.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Server.Service.imp
{
    public class ManageEmployee : EmployeeBase
    {
        #region Declaration
        private readonly SqlDataAccess db = new SqlDataAccess();        
        DataTable dtContainer;
        #endregion
        public override Employee Login(string username, string password)
        {
            Employee emp = new Employee();
            try
            {               
                string query = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.EmailAddress, e.UserID, 1 as RoleID, ISNULL(r.RoleName,'Admin') as Role " +
                               "FROM Employees e LEFT JOIN EmployeeRoles er ON e.EmployeeID = er.EmployeeID Left JOIN Roles r ON er.RoleID = r.RoleID " +
                               "WHERE e.EmailAddress='" + username + "' and e.UserID='" + password + "'";
                dtContainer = db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    emp = GenerateSQL.GetItem<Employee>(dtContainer.Rows[0]);
                }
            }
            catch(Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return emp;
            
        }

        public override List<Employee> GetEmployees()
        {
            List<Employee> retval = new List<Employee>();
            dtContainer = new DataTable();
            try
            {
                string query = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.EmailAddress, e.UserID, r.RoleID, ISNULL(r.RoleName, '') as Role " +
                               "FROM Employees e LEFT JOIN EmployeeRoles er ON e.EmployeeID = er.EmployeeID Left JOIN Roles r ON er.RoleID = r.RoleID " + 
                               "Order By [LastName] ASC";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Employee>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override Employee GetEmployeeById(int id)
        {
            Employee retval = null;
            dtContainer = new DataTable();
            try
            {
                string query = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.EmailAddress, e.UserID, r.RoleID, ISNULL(r.RoleName, '') as Role " +
                               "FROM Employees e LEFT JOIN EmployeeRoles er ON e.EmployeeID = er.EmployeeID Left JOIN Roles r ON er.RoleID = r.RoleID " +
                               "WHERE e.EmployeeID=" + id + "";
                dtContainer = db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    retval = GenerateSQL.GetItem<Employee>(dtContainer.Rows[0]);
                }
            }
            catch (Exception ex)
            {
            }
            return retval;
        }
    }


}
