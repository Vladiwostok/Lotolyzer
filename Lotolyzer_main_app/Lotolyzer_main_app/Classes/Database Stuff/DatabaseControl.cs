using System.Data;
using System.Data.SqlClient;

namespace Lotolyzer_main_app
{
    public static class DatabaseControl
    {
        /// <summary>
        /// The path to the database
        /// </summary>
        public static string Path = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
            System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Database1.mdf;Integrated Security=True";

        /// <summary>
        /// Makes a new connection to the database if there isn't one already
        /// </summary>
        /// <returns></returns>
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(Path);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        /// <summary>
        /// Extracts a data table based on a given query
        /// </summary>
        /// <param name="Query">The query to be executed</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string Query)
        {
            SqlConnection connection = OpenConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(Query, connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Executes a given query
        /// </summary>
        /// <param name="Query">The query to execute</param>
        public static void ExecuteQuery(string Query)
        {
            SqlConnection connection = OpenConnection();
            SqlCommand command = new SqlCommand(Query, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public static void CloseConnection()
        {
            SqlConnection connection = new SqlConnection(Path);

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
