using System.Collections.Generic;
using System.IO;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Class for doing basic checks on the database
    /// </summary>
    public static class DatabaseMainChecks
    {
        /// <summary>
        /// A list containing the base tables
        /// </summary>
        public static List<string> TableList = new List<string>() { "MainTable", "DrawTable", "NumberTable" };

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
            DatabaseControl.ExecuteQuery("BULK INSERT dbo.MainTable FROM '" + bulkDataFile + "' WITH(FIELDTERMINATOR = ' ')");
        }

        /// <summary>
        /// Checks if the database has a table
        /// </summary>
        /// <param name="TableName">The table to search for; specify the full name as in the database (MainTable, DrawTable, ...)</param>
        /// <returns>The existance of the table</returns>
        public static bool HasTable(string TableName)
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
        public static void CreateTable(string TableName)
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
        public static void DeleteTable(string TableName)
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
    }
}
