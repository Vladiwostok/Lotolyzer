using System.Data;
using System.Data.SqlClient;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Main control class for the database
    /// </summary>
    public static class DatabaseControl
    {
        #region public members

        /// <summary>
        /// The path to the database
        /// </summary>
        public static string Path = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
            System.AppDomain.CurrentDomain.BaseDirectory +
            "Resources\\Database\\LotolyzerDatabase.mdf;Integrated Security=True";

        #endregion

        #region Query methods

        /// <summary>
        /// Extracts a data table based on a given query
        /// </summary>
        /// <param name="Query">The query to be executed</param>
        /// <returns>The data table with the queried information</returns>
        public static DataTable GetDataTable(string Query)
        {
            // Open a connection to the database
            SqlConnection connection = OpenConnection();

            // Prepare the adapter
            SqlDataAdapter adapter = new SqlDataAdapter(Query, connection);

            // The data table to be returned
            DataTable table = new DataTable();

            // Fill the data table
            adapter.Fill(table);

            // Close the connection after finishing
            CloseConnection(connection);

            return table;
        }

        /// <summary>
        /// Executes a given query
        /// </summary>
        /// <param name="Query">The query to execute</param>
        public static void ExecuteQuery(string Query)
        {
            // The connection to the database
            SqlConnection connection = OpenConnection();

            // The command to be executed
            SqlCommand command = new SqlCommand(Query, connection);

            // Execute the command
            command.ExecuteNonQuery();

            // Close the connection after finishing
            CloseConnection(connection);
        }

        /// <summary>
        /// Executes a given query and returns it's result
        /// </summary>
        /// <param name="Query">The query to be executed</param>
        /// <returns>The result of the query</returns>
        public static int ExecuteResultQuery(string Query)
        {
            // The connection to the database
            SqlConnection connection = OpenConnection();

            // The command to be executed
            SqlCommand command = new SqlCommand(Query, connection);

            // Execute the query and save the result
            int result = (int)command.ExecuteScalar();

            // Close the connection after finishing
            CloseConnection(connection);

            return result;
        }

        public static void InsertRow(string TableName, object[] Data)
        {
            // Abort if the given data row is null or empty
            if (Data.Length == 0 || Data == null)
                return;

            // Make a string with the constant query
            string query = "SET DATEFORMAT DMY; INSERT INTO \"" + TableName + "\" VALUES(";

            // Add each value from data in the query
            foreach (object item in Data)
                query += "\'" + item.ToString() + "\'" + ",";

            // Remove the last ','
            query = query.Remove(query.Length - 1);

            // Finish building the query
            query += ");";

            ExecuteQuery(query);
        }

        #endregion

        #region Connection methods

        /// <summary>
        /// Opens the connection to the database
        /// </summary>
        /// <returns>The openned connection to the database</returns>
        public static SqlConnection OpenConnection()
        {
            // The connection to the database
            var connection = new SqlConnection(Path);

            // Check if the connection is already open, and if not, open it
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public static void CloseConnection(SqlConnection Connection)
        {
            // The connection to the database
            //var connection = new SqlConnection(Path);

            // Check if the connection is closed, and if not, close it
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }

        #endregion
    }
}
