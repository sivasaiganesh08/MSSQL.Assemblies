using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APWReferenceData
{
    public static class DBHelpers
    {
        /*
        private static readonly String phxConnectionString = "Initial Catalog = PHOENIX; Server=10.10.88.61; Integrated Security=SSPI;";
        private static readonly String apwConnectionString = "context connection=true";
        public static int TableExists(string tableName, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(@"IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table) SELECT 1 ELSE SELECT 0", connection);
            cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = tableName;
            int exists = (int)cmd.ExecuteScalar();
            return exists;
        }

        public static string CreateTable(string tableName, DataTable dataTable, SqlConnection connection)
        {
            string sqlsc;
            sqlsc = "CREATE TABLE " + tableName + "(";
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                sqlsc += "\n [" + dataTable.Columns[i].ColumnName + "] ";
                string columnType = dataTable.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", dataTable.Columns[i].MaxLength == -1 ? "max" : dataTable.Columns[i].MaxLength.ToString());
                        break;
                }
                if (dataTable.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + dataTable.Columns[i].AutoIncrementSeed.ToString() + "," + dataTable.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!dataTable.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }*/
    }
}
