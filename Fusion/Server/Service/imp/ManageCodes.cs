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
    public class ManageCodes : CodesBase
    {
        #region Declaration
        private readonly SqlDataAccess db = new SqlDataAccess();        
        DataTable dtContainer;
        #endregion
        public override List<tblCode> GetCodesByType(string key)
        {
            List<tblCode> retval = new List<tblCode>();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from tblCodes where CodeType='" + key + "'";
                dtContainer = db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<tblCode>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

    }


}
