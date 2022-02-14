using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Server.Helper
{
    public class SqlDataAccess
    {
        public String ConnectionString = string.Empty;
        public SqlConnection connection;
        #region Initiallize  
        public SqlDataAccess()
        {
            GetConnection();
          
        }
        
        public SqlConnection GetConnection()
        {
            //string cnstr = ConfigurationManager.ConnectionStrings["GPlusEntities"].ToString();
            connection = new SqlConnection("data source=38.17.55.191;initial catalog=Fusion;persist security info=True;user id=sa;password=BigNone123;");
            //  connection = new SqlConnection("data source=DESKTOP-AQKURQT\\SQLEXPRESS;initial catalog=GeneratorPlus;persist security info=True;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            return connection;
        }
        public SqlConnection GetConnection(string DatabaseName)
        {

            connection = new SqlConnection("data source=38.17.55.191;initial catalog=" + DatabaseName + ";persist security info=True;user id=sa;password=BigNone123;");
            return connection;
        }

        #endregion
        #region DB ConnectionOpen  
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
            }
            return false;
        }
        #endregion
        private string FleetPlusConnectionString()
        {
            return "data source=ppanadb01;initial catalog=Fleet Plus;persist security info=True;User ID=apps;Password=Ana@))*;";
        }
        private string CSAllConnectionString()
        {
            return "data source=ppanadb01;initial catalog=cs_all;persist security info=True;User ID=apps;Password=Ana@))*;";
        }
        public SqlDataAccess(string connectionname)
        {
            switch (connectionname)
            {
                case "CSAll":
                    connection = new SqlConnection(this.CSAllConnectionString());
                    break;
                case "FleetPlus":
                    connection = new SqlConnection(this.FleetPlusConnectionString());
                    break;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        public int ExecuteNonQuery_IUD(String Querys)
        {
            int result = 0;
            connection.Close();
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }

        public int ExecuteNonQuery_Command(SqlCommand cmd)
        {
            int result = 0;
            if (OpenConnection() == true)
            {
                result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }
        #region Dataset   
        public DataSet DataSet_return(String Querys)
        {
            DataSet ds = new DataSet();
            if (OpenConnection() == true)
            {
                SqlCommand cmdSel = new SqlCommand(Querys, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmdSel);
                da.Fill(ds);
                CloseConnection();
            }
            return ds;
        }
        #endregion
        public DataTable DataTable_return(string Query)
        {
            CloseConnection();
            DataTable dt = new DataTable();
            if (OpenConnection() == true)
            {
                using (SqlCommand myCommand = new SqlCommand(Query, connection))
                {
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        dt.Load(myReader);
                        CloseConnection();
                        return dt;
                    }
                }
            }
            return dt;
        }

        public DataSet GetDataSetByProcedure(string ProcName, params SqlParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            if (OpenConnection() == true)
            {
                SqlCommand cmdSel = new SqlCommand(ProcName, connection);
                cmdSel.CommandType = CommandType.StoredProcedure;
                AssignParameterValues(commandParameters, commandParameters);
                AttachParameters(cmdSel, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmdSel);
                da.Fill(ds);
                CloseConnection();
                return ds;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetDataTableByQuery(string query, params SqlParameter[] commandParameters)
        {
            DataTable ds = new DataTable();
            if (OpenConnection() == true)
            {
                SqlCommand cmdSel = new SqlCommand(query, connection);
                cmdSel.CommandType = CommandType.Text;
                AssignParameterValues(commandParameters, commandParameters);
                AttachParameters(cmdSel, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmdSel);
                da.Fill(ds);
                CloseConnection();
                return ds;
            }
            else
            {
                return null;
            }
        }

        public int ExecuteNonQuery(String Querys, params SqlParameter[] commandParameters)
        {
            int result = 0;
            if (OpenConnection() == true)
            {
                SqlCommand cmd = new SqlCommand(Querys, connection);
                cmd.CommandType = CommandType.Text;
                AssignParameterValues(commandParameters, commandParameters);
                AttachParameters(cmd, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                result = da.SelectCommand.ExecuteNonQuery();
                CloseConnection();
            }
            return result;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                return;
            }
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }
    }
}
