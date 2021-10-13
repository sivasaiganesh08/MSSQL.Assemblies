using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Microsoft.SqlServer.Server;

using static APWReferenceData.ReferenceDataQueries;
using static APWReferenceData.ReferenceDataTables;

public partial class StoredProcedures
{
    private static readonly String phxConnectionString = "Initial Catalog = PHOENIX; Server=10.10.88.61; Integrated Security=SSPI;";
    private static readonly String phxExtConnectionString = "Initial Catalog = PhoenixExt; Server=10.10.88.61; Integrated Security=SSPI;";
    private static readonly String apwConnectionString = "Initial Catalog = Ref_Data; Server=10.10.88.44; Integrated Security=SSPI;";
    private static readonly String apwRefConnectionString = "Initial Catalog = APWRefData; Server=10.10.88.44; Integrated Security=SSPI;";
    private static void CopyDataFromOneToOther(String sConnStr, DataTable dt, String sTableName)
    {
        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sConnStr, SqlBulkCopyOptions.TableLock))
        {
            bulkCopy.DestinationTableName = sTableName;
            bulkCopy.WriteToServer(dt);
        }
    }
        
    private static void UpdateTable(string tableName, string queryString, string connectionString)
    {
        using (SqlConnection phxConn = new SqlConnection(connectionString))
        {
            // Get Current Data
            SqlCommand Data = new SqlCommand(queryString, phxConn);
            SqlDataAdapter SqAdptr = new SqlDataAdapter(Data);
            DataSet SqDataset = new DataSet();
            SqAdptr.Fill(SqDataset);
            DataTable sqTable = SqDataset.Tables[0];
            
            // Transfer to New Table
            DataTable NewTable = new DataTable();
            NewTable = sqTable;
            CopyDataFromOneToOther(apwConnectionString, sqTable, tableName);
        }
    }

    private static void FillRows(object ReferenceObject, out string TableName, out int Added, out int Updated, out string Error)
    {
        RefResponse refData = (RefResponse)ReferenceObject;
        TableName = refData.TableName;
        Added = refData.RecordsAdded;
        Updated = refData.RecordsUpdated;
        Error = refData.ErrorDescription;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read, Name = "UpdateReferenceData", FillRowMethodName = "FillRows", TableDefinition = "TableName nvarchar(50), Added int, Updated int, Error nvarchar(800)")]
    public static IEnumerable UpdateReferenceData()
    {
        ArrayList ResponseCollection = new ArrayList
        {
            ProcessReferenceData(GetAccountPurposes, phxConnectionString, "AccountPurpose"),
            ProcessReferenceData(GetRates, phxConnectionString, "AccountRateIndex"),
            ProcessReferenceData(GetAccountTypes, phxConnectionString, "AccountType"),
            ProcessReferenceData(GetAddressTypes, phxConnectionString, "AddressType"),
            ProcessReferenceData(GetApplicationTypes, phxConnectionString, "ApplicationType"),
            ProcessReferenceData(GetBranches, phxConnectionString, "Branch"),
            ProcessReferenceData(GetChargeCodes, phxConnectionString, "ChargeCode"),
            ProcessReferenceData(GetCountries, phxConnectionString, "Country"),
            ProcessReferenceData(GetCities, phxConnectionString, "City"),
            ProcessReferenceData(GetCollateralTypes, phxConnectionString, "CollateralType"),
            ProcessReferenceData(GetRiskTypes, phxConnectionString, "CreditRiskCode"),
            ProcessReferenceData(GetCurrencies, phxConnectionString, "Currency"),
            ProcessReferenceData(GetCycleCodes, phxConnectionString, "CycleCodes"),
            ProcessReferenceData(GetDPClasses, phxConnectionString, "DepositClasses"),
            ProcessReferenceData(GetDPClassInt, phxConnectionString, "DepositClassesInt"),
            ProcessReferenceData(GetEducation, phxConnectionString, "Education"),
            ProcessReferenceData(GetEmployees, phxConnectionString, "Employee"),
            ProcessReferenceData(GetEmployment, phxConnectionString, "Employment"),
            ProcessReferenceData(GetCategories, phxConnectionString, "EmploymentCategory"),
            ProcessReferenceData(GetEscrowAgents, phxConnectionString, "EscrowAgent"),
            ProcessReferenceData(GetSourceOfFunds, phxConnectionString, "FundSource"),
            ProcessReferenceData(GetLNClasses, phxConnectionString, "LoanClasses"),
            ProcessReferenceData(GetLNClassInt, phxConnectionString, "LoanClassesInt"),
            ProcessReferenceData(GetMaritalStatuses, phxExtConnectionString, "MaritalStatus"),
            ProcessReferenceData(GetMarketingLanguages, phxConnectionString, "Marketing"),
            ProcessReferenceData(GetMaritalTypes, phxConnectionString, "MarriageType"),
            ProcessReferenceData(GetODRelatedClassesCounts, phxConnectionString, "OverdraftCounters"),
            ProcessReferenceData(GetReasons, phxConnectionString, "Reason"),
            ProcessReferenceData(GetRelationships, phxConnectionString, "Relationship"),
            ProcessReferenceData(GetRegions, phxConnectionString, "Region"),
            ProcessReferenceData(GetSICCodes, phxConnectionString, "SICCodes"),
            ProcessReferenceData(GetTerritories, phxConnectionString, "Territory"),
            ProcessReferenceData(GetTitles, phxConnectionString, "Title"),
            ProcessReferenceData(GetUserDefValues, phxConnectionString, "UserDefFields"),
            ProcessReferenceData(GetRestrictionLevels, phxConnectionString, "RIMAccessRestriction"),
            ProcessReferenceData(GetRIMUserDefinedValues, phxConnectionString, "RIMUserDefinedFields"),
            ProcessReferenceData(GetRoutingNumbers, phxConnectionString, "RoutingNumbers"),
            ProcessReferenceData(GetDPClass, phxConnectionString, "DPClass"),
            ProcessReferenceData(GetDPClassRim, phxConnectionString, "DPClassRim"),
            ProcessReferenceData(GetDPUserDefined, phxConnectionString, "DPUserDefined"),
            ProcessReferenceData(GetLNClass, phxConnectionString, "LNClass"),
            ProcessReferenceData(GetLNUserDefined, phxConnectionString, "LNUserDefined"),
            ProcessReferenceData(GetLNClassRim, phxConnectionString, "LNClassRim"),
            ProcessReferenceData(GetRMClass, phxConnectionString, "RMClass")
        };

        return ResponseCollection;
    }

    public static DataTable Sort(DataTable dt, string colName, string direction)
    {
        dt.DefaultView.Sort = colName + " " + direction;
        return dt.DefaultView.ToTable();
    }

    static RefResponse ProcessReferenceData(string phxQuery, string PhoenixConnectionString, string apwTableName)
    {
        int rowsAdded = 0;
        int rowsUpdated = 0;
        DataSet phxData = ProcessPhoenixData(phxQuery, PhoenixConnectionString);
        DataSet apwData = ProcessAppworksData(apwTableName);

        DataTable phxTable = Sort(phxData.Tables[0], phxData.Tables[0].Columns[0].ColumnName, "ASC");
        DataTable apwTable = Sort(apwData.Tables[0], apwData.Tables[0].Columns[0].ColumnName, "ASC");

        apwTable.PrimaryKey = new DataColumn[] { apwTable.Columns[0], apwTable.Columns[1] };

        string selectQuery = "SELECT * FROM " + apwTableName;
        using (SqlConnection connection = new SqlConnection(apwConnectionString))
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(selectQuery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                DataTableMapping mapping = adapter.TableMappings.Add(apwTableName, apwTableName);
                for (int i = 0; i < phxTable.Columns.Count; i++)
                {
                    mapping.ColumnMappings.Add(phxTable.Columns[i].ToString(), apwTable.Columns[i].ToString());
                }
                connection.Open();
                builder.GetUpdateCommand();
                builder.GetInsertCommand();
                for (int j = 0; j < phxTable.Rows.Count; j++)
                {
                    DataRow ToUpdate = apwTable.Rows.Find(new object[] { phxTable.Rows[j][0], phxTable.Rows[j][1] });

                    if (ToUpdate == null)
                    {
                        DataRow ToInsert = apwTable.NewRow();
                        for (int k = 0; k < phxTable.Columns.Count; k++)
                        {
                            ToInsert[k] = phxTable.Rows[j][k];
                        }
                        apwTable.Rows.Add(ToInsert);
                        rowsAdded++;
                    }
                    else
                    {
                        ToUpdate.AcceptChanges();
                        if (!apwTable.Rows[j].Equals(phxTable.Rows[j]))
                        {
                            for (int k = 1; k < phxTable.Columns.Count; k++)
                            {
                                ToUpdate[k] = phxTable.Rows[j][k];
                            }
                            rowsUpdated++;
                        }
                    }
                }
                adapter.Update(apwTable);
                RefResponse response = new RefResponse(apwTableName, rowsAdded, rowsUpdated, "None");
                return response;
            }
            catch (Exception ex)
            {
                RefResponse response = new RefResponse(apwTableName, -1, -1, ex.Message);
                return response;
            }
        }
    }

    public static DataSet ProcessPhoenixData(string phxQuery, string connectionString)
    {
        using (SqlConnection phxConn = new SqlConnection(connectionString))
        {
            SqlCommand phxData = new SqlCommand(phxQuery, phxConn);
            SqlDataAdapter phxAdapter = new SqlDataAdapter(phxData);
            DataSet phxDataset = new DataSet();
            phxAdapter.Fill(phxDataset);
            return phxDataset;
        }
    }

    public static DataSet ProcessAppworksData(string tableName)
    {
        string selectQuery = "SELECT * FROM " + tableName;
        using (SqlConnection connection = new SqlConnection(apwConnectionString))
        {
            SqlCommand apwData = new SqlCommand(selectQuery, connection);
            SqlDataAdapter apwAdapter = new SqlDataAdapter(apwData);
            DataSet apwDataset = new DataSet();
            apwAdapter.Fill(apwDataset);
            return apwDataset;
        }
    }

    [SqlProcedure]
    public static void UpdateAddressTypes(string tableName)
    {
        UpdateTable(tableName, GetAddressTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCountries(string tableName)
    {
        UpdateTable(tableName, GetCountries, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCities(string tableName)
    {
        UpdateTable(tableName, GetCities, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRegions(string tableName)
    {
        UpdateTable(tableName, GetRegions, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateTerritories(string tableName)
    {
        UpdateTable(tableName, GetTerritories, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateTitles(string tableName)
    {
        UpdateTable(tableName, GetTitles, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateAccountPurposes(string tableName)
    {
        UpdateTable(tableName, GetAccountPurposes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCurrencies(string tableName)
    {
        UpdateTable(tableName, GetCurrencies, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateEducation(string tableName)
    {
        UpdateTable(tableName, GetEducation, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateApplicationTypes(string tableName)
    {
        UpdateTable(tableName, GetApplicationTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateAccountTypes(string tableName)
    {
        UpdateTable(tableName, GetAccountTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateSourceOfFunds(string tableName)
    {
        UpdateTable(tableName, GetSourceOfFunds, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCollateralTypes(string tableName)
    {
        UpdateTable(tableName, GetCollateralTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateMarriageTypes(string tableName)
    {
        UpdateTable(tableName, GetMaritalTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateMarketingLanguages(string tableName)
    {
        UpdateTable(tableName, GetMarketingLanguages, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRelationships(string tableName)
    {
        UpdateTable(tableName, GetRelationships, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRIMClasses(string tableName)
    {
        UpdateTable(tableName, GetRIMClasses, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateEmployment(string tableName)
    {
        UpdateTable(tableName, GetEmployment, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateReasons(string tableName)
    {
        UpdateTable(tableName, GetReasons, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateEmploymentCategories(string tableName)
    {
        UpdateTable(tableName, GetCategories, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateBranches(string tableName)
    {
        UpdateTable(tableName, GetBranches, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateSICCodes(string tableName)
    {
        UpdateTable(tableName, GetSICCodes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCreditRiskCodes(string tableName)
    {
        UpdateTable(tableName, GetRiskTypes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateEmployees(string tableName)
    {
        UpdateTable(tableName, GetEmployees, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRestrictionLevels(string tableName)
    {
        UpdateTable(tableName, GetRestrictionLevels, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateEscrowAgents(string tableName)
    {
        UpdateTable(tableName, GetEscrowAgents, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateCycleCodes(string tableName)
    {
        UpdateTable(tableName, GetCycleCodes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRates(string tableName)
    {
        UpdateTable(tableName, GetRates, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateChargeCodes(string tableName)
    {
        UpdateTable(tableName, GetChargeCodes, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateDPClasses(string tableName)
    {
        UpdateTable(tableName, GetDPClasses, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateDPClassInt(string tableName)
    {
        UpdateTable(tableName, GetDPClassInt, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRIMUserDefinedValues(string tableName)
    {
        UpdateTable(tableName, GetRIMUserDefinedValues, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateUserDefValues(string tableName)
    {
        UpdateTable(tableName, GetUserDefValues, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateODClassCounts(string tableName)
    {
        UpdateTable(tableName, GetODRelatedClassesCounts, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateLNClasses(string tableName)
    {
        UpdateTable(tableName, GetLNClasses, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateLNClassInt(string tableName)
    {
        UpdateTable(tableName, GetLNClassInt, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateMaritalStatus(string tableName)
    {
        UpdateTable(tableName, GetMaritalStatuses, phxExtConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRoutingNumbers(string tableName)
    {
        UpdateTable(tableName, GetRoutingNumbers, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateDPClassRim(string tableName)
    {
        UpdateTable(tableName, GetDPClassRim, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateLNClassRim(string tableName)
    {
        UpdateTable(tableName, GetLNClassRim, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateRMClass(string tableName)
    {
        UpdateTable(tableName, GetRMClass, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateDPClass(string tableName)
    {
        UpdateTable(tableName, GetDPClass, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateLNClass(string tableName)
    {
        UpdateTable(tableName, GetLNClass, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateDPUserDefined(string tableName)
    {
        UpdateTable(tableName, GetDPUserDefined, phxConnectionString);
    }

    [SqlProcedure]
    public static void UpdateLNUserDefined(string tableName)
    {
        UpdateTable(tableName, GetLNUserDefined, phxConnectionString);
    }
}
