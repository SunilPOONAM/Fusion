using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fusion.Server.Helper
{
    public class GenerateSQL
    {
        public static string BuildAllFieldsSQL(DataTable table)
        {
            string sql = "";
            foreach (DataColumn column in table.Columns)
            {
                if (sql.Length > 0)
                    sql += ", ";
                sql += column.ColumnName;
            }
            return sql;
        }

        // Returns a SQL INSERT command. Assumes autoincrement is identity (optional)

        public static string BuildInsertSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO " + table.TableName + " (");
            StringBuilder values = new StringBuilder("VALUES (");
            bool bFirst = true;
            bool bIdentity = false;
            string identityType = null;
            string columnQuots = null;

            foreach (DataColumn column in table.Columns)
            {
                if (table.Columns.IndexOf(column) != 0)
                {
                    if (column.AutoIncrement)
                    {
                        bIdentity = true;

                        switch (column.DataType.Name)
                        {
                            case "Int16":
                                identityType = "smallint";
                                break;
                            case "SByte":
                                identityType = "tinyint";
                                break;
                            case "Int64":
                                identityType = "bigint";
                                break;
                            case "Decimal":
                                identityType = "decimal";
                                break;
                            default:
                                identityType = "int";
                                break;
                        }
                    }
                    else
                    {
                        if (bFirst)
                            bFirst = false;
                        else
                        {
                            sql.Append(", ");
                            //values.Append(", ");
                        }

                        sql.Append(column.ColumnName);
                        //values.Append("@");
                        //values.Append(column.ColumnName);
                    }
                }
            }
            bFirst = true;
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (count > 0)
                    {
                        switch (row.Table.Columns[count].DataType.Name)
                        {
                            case "Int16":
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "SByte":
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "Int64":
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "Decimal":
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "DateTime":
                                var StringDate = item != DBNull.Value ? Convert.ToDateTime(item).ToString("MM-dd-yyyy") : "";
                                values.Append("'");
                                values.Append(StringDate);
                                values.Append("'");
                                values.Append(",");
                                break;
                            default:
                                values.Append("'");
                                values.Append(item);
                                values.Append("',");
                                break;
                        }
                    }
                    count++;
                }
            }
            values.Remove(values.Length - 1, 1);
            sql.Append(") ");
            sql.Append(values.ToString());
            sql.Append(")");

            if (bIdentity)
            {
                sql.Append("; SELECT CAST(scope_identity() AS ");
                sql.Append(identityType);
                sql.Append(")");
            }

            return sql.ToString(); ;
        }

        // Creates a SqlParameter and adds it to the command

        public static void InsertParameter(SqlCommand command,
                                             string parameterName,
                                             string sourceColumn,
                                             object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, value);

            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.SourceColumn = sourceColumn;
            parameter.SourceVersion = DataRowVersion.Current;

            command.Parameters.Add(parameter);
        }

        // Creates a SqlCommand for inserting a DataRow
        public static SqlCommand CreateInsertCommand(DataRow row)
        {
            DataTable table = row.Table;
            string sql = BuildInsertSQL(table);
            SqlCommand command = new SqlCommand(sql);
            command.CommandType = System.Data.CommandType.Text;

            foreach (DataColumn column in table.Columns)
            {
                if (!column.AutoIncrement)
                {
                    string parameterName = "@" + column.ColumnName;
                    InsertParameter(command, parameterName,
                                      column.ColumnName,
                                      row[column.ColumnName]);
                }
            }
            return command;
        }

        // Inserts the DataRow for the connection, returning the identity
        public static object InsertDataRow(DataRow row, string connectionString)
        {
            SqlCommand command = CreateInsertCommand(row);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public static string BuildUpdateSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("UPDATE " + table.TableName + " SET ");
            StringBuilder values = new StringBuilder();
            bool bFirst = true;
            string where = null;
            bool bIdentity = false;
            string identityType = null;

            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (count > 0)
                    {
                        switch (row.Table.Columns[count].DataType.Name)
                        {
                            case "Int16":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "SByte":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "Int64":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "Decimal":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                values.Append(item);
                                values.Append(",");
                                break;
                            case "DateTime":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                var StringDate = item!=DBNull.Value? Convert.ToDateTime(item).ToString("MM-dd-yyyy"):"";
                                values.Append("'");
                                values.Append(StringDate);
                                values.Append("'");
                                values.Append(",");
                                break;
                            default:
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append("=");
                                values.Append("'");
                                values.Append(item);
                                values.Append("',");
                                break;
                        }
                    }
                    count++;
                    where = " Where " + row.Table.Columns[0].ColumnName + "=" + row.ItemArray[0];
                }
            }
            values.Remove(values.Length - 1, 1);
            sql.Append(values.ToString());
            sql.Append(where);
            return sql.ToString(); ;
        }

        public static string BuildDeleteSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("Delete from " + table.TableName);
            string where = null;
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    count++;
                    where = " Where " + row.Table.Columns[0].ColumnName + "=" + row.ItemArray[0];
                }
            }
            sql.Append(where);
            return sql.ToString(); ;
        }

        public static string BuildSelectSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("SELECT * ");
            sql.Append(" FROM " + table.TableName);
            return sql.ToString(); ;
        }

        public static string BuildSelectWithWhereSQL(DataTable table)
        {
            StringBuilder sql = new StringBuilder("Select ");
            StringBuilder values = new StringBuilder();
            string where = null;
            int count = 0;
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (count > 0)
                    {
                        switch (row.Table.Columns[count].DataType.Name)
                        {
                            case "Int16":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append(",");
                                break;
                            case "SByte":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append(",");
                                break;
                            case "Int64":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append(",");
                                break;
                            case "Decimal":
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append(",");
                                break;
                            default:
                                values.Append(row.Table.Columns[count].ColumnName);
                                values.Append(",");
                                break;
                        }
                    }
                    count++;
                    where = " Where " + row.Table.Columns[0].ColumnName + "=" + row.ItemArray[0];
                }
            }
            values.Remove(values.Length - 1, 1);
            sql.Append(values.ToString());
            sql.Append(" FROM " + table.TableName);
            sql.Append(where);
            return sql.ToString(); ;
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                //table.Columns.Add(prop.Name, prop.PropertyType);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            columnNames = columnNames.Select(m => m.Replace(" ", "_")).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            if (row[pro.Name] != DBNull.Value)
                                pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                return objT;
            }).ToList();
        }

        
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            var columnNames = dr.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            columnNames = columnNames.Select(m => m.Replace(" ", "_")).ToList();
            var properties = typeof(T).GetProperties();
            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name.ToLower()))
                {
                    try
                    {
                        if (dr[pro.Name] != DBNull.Value)
                            pro.SetValue(obj, dr[pro.Name]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return obj;
        }
    }
}
