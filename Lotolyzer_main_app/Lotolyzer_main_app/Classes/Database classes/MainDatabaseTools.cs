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

            // Calculate the DrawTable and NumberTable
            InsertTables();
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
        /// Inserts the required data into the tables
        /// </summary>
        public static void InsertTables()
        {
            var dataTable = DatabaseControl.GetDataTable("SELECT * FROM MainTable");

            NumberAnalysis numberAnalysis = new NumberAnalysis();

            // Calculate and instert each row in draw table
            foreach(DataRow row in dataTable.Rows)
            {
                numberAnalysis.Update(row);

                DrawAnalysis drawAnalysis = new DrawAnalysis(row);

                drawAnalysis.Analyze();

                DatabaseControl.InsertRow("DrawTable", drawAnalysis.DrawRow);
            }

            // Insert the number table
            for (int number = 1; number < 50; number++)
            {
                object[] temp = new object[6];

                // Id and frequency
                for (int idx = 0; idx < 2; idx++)
                    temp[idx] = numberAnalysis.NumberRow[number, idx];

                // Frequency %
                temp[2] = 100 * (double)numberAnalysis.NumberRow[number, 1] / numberAnalysis.LastDraw;

                // Last delay
                temp[3] = numberAnalysis.NumberRow[number, 2];
                // Max delays
                temp[4] = numberAnalysis.NumberRow[number, 3];

                // Average delay
                temp[5] = (double)numberAnalysis.NumberRow[number, 4] / (double)numberAnalysis.NumberRow[number, 5];

                DatabaseControl.InsertRow("NumberTable", temp);
            }
        }

        #endregion
    }
}
