namespace APWReferenceData
{
    class ReferenceDataTables
    {
        public class RefResponse
        {
            public string TableName;
            public int RecordsAdded;
            public int RecordsUpdated;
            public string ErrorDescription;
            public RefResponse(string tableName, int recordsAdded, int recordsUpdated, string errorDescription)
            {
                TableName = tableName;
                RecordsAdded = recordsAdded;
                RecordsUpdated = recordsUpdated;
                ErrorDescription = errorDescription;
            }
        }
    }
}
