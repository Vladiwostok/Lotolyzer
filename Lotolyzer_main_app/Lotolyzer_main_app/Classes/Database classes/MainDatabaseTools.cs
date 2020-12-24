using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Class for doing basic checks on the database
    /// </summary>
    /// To do: add integrity checks
    public static class MainDatabaseTools
    {
        #region public members

        /// <summary>
        /// A list containing the base tables
        /// </summary>
        public static List<string> TableList = new List<string>() { "MainTable", "DrawTable", "NumberTable" };

        #endregion

        /// <summary>
        /// Perfoms a full reset on the database, deleting all the tables and creating them from scratch;
        /// be very careful when using this!
        /// </summary>
        public static void FullReset()
        {
            // Delete and re-create each base table
            foreach(var table in TableList)
            {
                DeleteTable(table);
                CreateTable(table);
            }

            // Set the file path to the raw text file containing the main table
            string bulkDataFile = System.AppDomain.CurrentDomain.BaseDirectory + "Resources\\Raw text files\\MainTable.txt";

            // Check if the file is there
            if (File.Exists(bulkDataFile) == false)
                throw new FileNotFoundException($"File not found : {bulkDataFile}");

            // Insert the whole file into the main table
            DatabaseControl.ExecuteQuery("SET DATEFORMAT DMY; BULK INSERT dbo.MainTable FROM '" + bulkDataFile + "' WITH(FIELDTERMINATOR = ' ')");
        }

        #region Helper methods

        /// <summary>
        /// Checks if the database has a table
        /// </summary>
        /// <param name="TableName">The table to search for; specify the full name as in the database (MainTable, DrawTable, ...)</param>
        /// <returns>The existance of the table</returns>
        private static bool HasTable(string TableName)
        {
            // The file name containing the relative file path from the base app directory
            string fileName = "Resources\\Database\\SQL scripts\\Check" + TableName + ".sql";

            // The query to be executed
            string query = DatabaseScripts.LoadScript(fileName);

            // If the query couldn't be loaded from the file, throw an exception
            if (query == null)
                throw new FileNotFoundException($"File not found : {fileName}");

            // Execute the query and save the result
            int res = DatabaseControl.ExecuteResultQuery(query);

            return res != 0;
        }

        /// <summary>
        /// Creates a table
        /// </summary>
        /// <param name="TableName">The table to create; specify the full name as in the database (MainTable, DrawTable, ...)</param>
        private static void CreateTable(string TableName)
        {
            // The file name containing the relative file path from the base app directory
            string fileName = "Resources\\Database\\SQL scripts\\Create" + TableName + ".sql";

            // The query to be executed
            string query = DatabaseScripts.LoadScript(fileName);

            // If the query couldn't be loaded from the file, throw an exception
            if (query == null)
                throw new FileNotFoundException($"File not found : {fileName}");

            // Execute the query
            DatabaseControl.ExecuteQuery(query);
        }

        /// <summary>
        /// Deletes a table
        /// </summary>
        /// <param name="TableName">The table to delete; specify the full name as in the database (MainTable, DrawTable, ...)</param>
        private static void DeleteTable(string TableName)
        {
            // The file name containing the relative file path from the base app directory
            string fileName = "Resources\\Database\\SQL scripts\\Delete" + TableName + ".sql";

            // The query to be executed
            string query = DatabaseScripts.LoadScript(fileName);

            // If the query couldn't be loaded from the file, throw an exception
            if (query == null)
                throw new FileNotFoundException($"File not found : {fileName}");

            // Execute the query
            DatabaseControl.ExecuteQuery(query);
        }

        /// <summary>
        /// Inserts the required data into 
        /// </summary>
        public static void InsertTables()
        {
            var dataTable = DatabaseControl.GetDataTable("SELECT * FROM MainTable");

            int[] freq = new int[50];
            int[] lastfreq = new int[50];
            int lastDraw = 0;

            foreach(DataRow row in dataTable.Rows)
            {
                lastDraw++;

                int id = (int)row.ItemArray[0];

                System.DateTime date = (System.DateTime)row.ItemArray[1];

                // The default values are 0
                int[] nums = new int[6];
                int[] linenum = new int[5];
                int[] colnum = new int[10];

                for (int idx = 0; idx < 6; idx++)
                {
                    nums[idx] = (byte)row.ItemArray[2 + idx];

                    linenum[(nums[idx] - 1) / 10]++;

                    colnum[nums[idx] % 10]++;
                }

                // Move this out of here
                using (System.Data.SqlClient.SqlConnection connection = DatabaseControl.OpenConnection())
                {
                    string query1 = "INSERT INTO DrawTable ([Draw Number], [Draw Date], [N1], [N2], [N3], [N4], [N5], [N6]) values(@id,@date,@n1,@n2,@n3,@n4,@n5,@n6)";
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query1, connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                        cmd.Parameters.Add("@n1", SqlDbType.TinyInt).Value = nums[0];
                        cmd.Parameters.Add("@n2", SqlDbType.TinyInt).Value = nums[1];
                        cmd.Parameters.Add("@n3", SqlDbType.TinyInt).Value = nums[2];
                        cmd.Parameters.Add("@n4", SqlDbType.TinyInt).Value = nums[3];
                        cmd.Parameters.Add("@n5", SqlDbType.TinyInt).Value = nums[4];
                        cmd.Parameters.Add("@n6", SqlDbType.TinyInt).Value = nums[5];

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #endregion
    }
}
