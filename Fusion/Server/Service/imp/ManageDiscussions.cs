using Fusion.Server.Helper;
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
    public class ManageDiscussions : DiscussionBase
    {
        #region Declaration
        private readonly SqlDataAccess _db = new SqlDataAccess();
        DataTable dtContainer;
        #endregion

        public override List<Discussion> GetDiscussions(string assignedObjectType, string id)
        {
            List<Discussion> retval = new List<Discussion>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Discussions where AssignedObjectType='" + assignedObjectType + "' and ObjectID=" + id;
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Discussion>(dtContainer);

            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override Discussion GetDiscussion(string id)
        {
            Discussion retval = new Discussion();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Discussions where DiscussionID=" + id;
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.GetItem<Discussion>(dtContainer.Rows[0]);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override bool AddDiscussion(Discussion od)
        {
            bool retval = false;
            int result = 0;
            try
            {
                string DateStamp = od.DateStamp != null ? Convert.ToDateTime(od.DateStamp).ToString("MM-dd-yyyy") : "";
                string InsertQuery = "insert into Discussions(AssignedObjectType,ObjectID,Contact,Summary,DateStamp) values ('" + od.AssignedObjectType + "'," + od.ObjectID + ",'" + od.Contact + "','" + od.Summary + "','" + DateStamp + "')";
                result = _db.ExecuteNonQuery_IUD(InsertQuery);
                if (result > 0)
                {
                    retval = true;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }
        public override bool UpdateDiscussion(Discussion od)
        {
            int result = 0;
            try
            {
                string query = "UPDATE Discussions SET Contact='" + od.Contact + "',Summary='" + od.Summary + "',DateStamp='" + od.DateStamp + "' Where DiscussionID=" + od.DiscussionID;
                result = _db.ExecuteNonQuery_IUD(query);
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}