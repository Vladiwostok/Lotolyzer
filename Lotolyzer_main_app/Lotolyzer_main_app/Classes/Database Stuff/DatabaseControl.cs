using System.Data;
using System.Data.SqlClient;

namespace Lotolyzer_main_app
{
    public static class DatabaseControl
    {
        #region public members

        /// <summary>
        /// The path to the database
        /// </summary>
        public static string Path = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
            System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\LotolyzerDatabase.mdf;Integrated Security=True";

        /// <summary>
        /// The connection to the database
        /// </summary>
        public static SqlConnection DataBaseConnection = new SqlConnection(Path);

        #endregion

        /// <summary>
        /// Extracts a data table based on a given query
        /// </summary>
        /// <param name="Query">The query to be executed</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string Query)
        {
            OpenConnection();

            SqlDataAdapter adapter = new SqlDataAdapter(Query, DataBaseConnection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            CloseConnection();

            return table;
        }

        /// <summary>
        /// Executes a given query
        /// </summary>
        /// <param name="Query">The query to execute</param>
        public static void ExecuteQuery(string Query)
        {
            OpenConnection();

            SqlCommand command = new SqlCommand(Query, DataBaseConnection);
            command.ExecuteNonQuery();

            CloseConnection();
        }

        #region Helper functions

        /// <summary>
        /// Opens the connection to the database
        /// </summary>
        /// <returns></returns>
        public static void OpenConnection()
        {
            if (DataBaseConnection.State == ConnectionState.Open)
                return;

            DataBaseConnection.Open();
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public static void CloseConnection()
        {
            if (DataBaseConnection.State == ConnectionState.Closed)
                return;

            DataBaseConnection.Close();
        }

        #endregion
    }
}
