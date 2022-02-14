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
    public class ManagePermissions : PermissionsBase
    {
        #region Declaration
        private readonly SqlDataAccess db = new SqlDataAccess();
        DataTable dtContainer;
        #endregion
        public override List<Role> GetRoleInfo()
        {
            List<Role> retval = new List<Role>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Roles";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Role>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }
        public override List<Page> GetPageInfo()
        {
            List<Page> retval = new List<Page>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Pages";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Page>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }
        public override List<Permission> GetPermissionInfo()
        {
            List<Permission> retval = new List<Permission>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Permissions";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Permission>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }
        public override List<PermissionsPerPage> GetPermissionsPerPage(int RoleId)
        {
            List<PermissionsPerPage> retval = new List<PermissionsPerPage>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select pp.*,p.PageURL from PermissionsPerPage pp join Pages p on p.PageID=pp.PageID where RoleID=" + RoleId + " ";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<PermissionsPerPage>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override ResponseModel AddPermissionPerPage(PermissionsPerPage model)
        {

            int result = 0;
            ResponseModel res = new ResponseModel();
            try
            {
                if (!CheckIfExists(model.PageID, model.PermissionID, model.RoleID))
                {
                    string query = "insert into PermissionsPerPage(PageID,PermissionID,DateAdded,AddedByID,RoleID) values (" + model.PageID + ",'" + model.PermissionID + "'," + DateTime.Now.ToString("MM/dd/yyyy") + ",'" + model.AddedByID + "','" + model.RoleID + "');select scope_identity();";
                    dtContainer = db.DataTable_return(query);
                    if (dtContainer.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dtContainer.Rows[0][0]);
                        if (result > 0)
                        {
                            res.Status = true;
                            res.Result = result;
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = "Error in saving record";
                        }
                    }
                }
                else
                {
                    res.Status = false;
                    res.Message = "Record already exists";
                }
            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = "Error in saving record";
            }
            return res;
        }

        public override bool DeletePermissionPerPage(int permissionPerPageId)
        {
            bool retval = false;
            int result = 0;
            try
            {
                string query = "delete from PermissionsPerPage where PermissionsPerPageID='" + permissionPerPageId + "'";
                result = db.ExecuteNonQuery_IUD(query);
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

        private bool CheckIfExists(int pageId, int permissionId, int RoleId)
        {
            bool retval = false;

            try
            {
                string query = "select * from PermissionsPerPage where PageId='" + pageId + "' and PermissionId='" + permissionId + "' and RoleId='" + RoleId + "'";
                dtContainer = db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
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
