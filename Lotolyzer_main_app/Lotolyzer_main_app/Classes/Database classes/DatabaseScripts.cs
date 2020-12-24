using System;
using System.IO;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Manipulates scripts stored in files
    /// </summary>
    public static class DatabaseScripts
    {
        /// <summary>
        /// Loads a script from a text file
        /// </summary>
        /// <param name="RelativeFilePath">The path to the file relative to the application's root directory</param>
        /// <returns>The whole content of the file, or null if any exceptions happen</returns>
        public static string LoadScript(string RelativeFilePath)
        {
            // The full path to the file
            string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + RelativeFilePath;

            try
            {
                // The loaded script from the text file
                string script = File.ReadAllText(fullFilePath);

                return script;
            }
            catch
            {
                // Return null if any exceptions happen
                return null;
            }
        }
    }
}
