using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Words.Api;

namespace Words.Output.Mssql
{
    /// <summary>
    /// Implementation for output to MS SQL base
    /// </summary>
    public class MsSqlOutput : IWordsOutput
    {
        public string Prefix
        {
            get { return "mssql"; }
        }

        public string Description
        {
            get { return "output data to Microsoft SQL DB, specify correct connection string as parameter"; }
        }

        public void Write(string destination, Dictionary<string, int> data)
        {
            if (string.IsNullOrEmpty(destination))
            {
                Console.WriteLine("Error: connection string is empty");
                return;
            }

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(destination);
                connection.Open();

                if (GetTables(connection).All(x => x != "Words"))
                {
                    CreateTable(connection);
                }

                foreach (KeyValuePair<string, int> item in data)
                {
                    string query = "INSERT INTO [Words] (Word, Count) VALUES (@p1, @p2)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@p1", item.Key);
                        command.Parameters.AddWithValue("@p2", item.Value);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }

        }

        /// <summary>
        /// Get tables list for base in connection
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private string[] GetTables(SqlConnection connection)
        {
            List<string> tables = new List<string>();

            const string query = "SELECT name FROM sys.Tables";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string table = reader["name"].ToString();
                        tables.Add(table);
                    }
                }
            }

            return tables.ToArray();
        }

        // Create simple table
        private void CreateTable(SqlConnection connection)
        {
            const string query =
                "CREATE TABLE [dbo].[Words] ([Id] int IDENTITY(1, 1) NOT NULL, [Word] nvarchar(max) NOT NULL,[Count] int NOT NULL, PRIMARY KEY CLUSTERED ([Id]))";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
