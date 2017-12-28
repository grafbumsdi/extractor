using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Extractor
{
    public class SqlDataReader
    {
        private const string ConnectionString =
            "Server=192.168.11.30;Database=wikifolio_core_production;User Id=wfreadonly;Password=wfreadonly;";

        public IEnumerable<IDictionary<string, object>> GetRows(string selectStatement)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                if (!selectStatement.StartsWith("SELECT"))
                {
                    throw new ArgumentException($"ILLEGAL STATEMENT!!! IT IS NOT A SELECT: '{selectStatement}'");
                }
                SqlCommand command = new SqlCommand(selectStatement, connection);
                connection.Open();
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        yield return Enumerable.Range(0, reader.FieldCount)
                            .ToDictionary(reader.GetName, reader.GetValue);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    // reader.Close();
                }
            }
        }
    }
}
