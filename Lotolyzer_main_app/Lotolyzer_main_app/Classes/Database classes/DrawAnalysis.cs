using System;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// The main class for doing draw analysis and insterting the results into the database
    /// </summary>
    public static class DrawAnalysis
    {
        // Doesn't work, stop
        /// <summary>
        /// Parses the given archive HTML source into useful entries
        /// </summary>
        /// <param name="SourceHTML">The source string to parse</param>
        /// <returns></returns>
        public static int ParseArchiveDraw(string SourceURL)
        {
            string sourceHTML = HTMLSource.DownloadAsString(SourceURL);

            if (sourceHTML == null)
                throw new ArgumentNullException("SourceHTML");

            int idx = sourceHTML.IndexOf("table class=bilet");

            sourceHTML = sourceHTML.Substring(idx);

            idx = sourceHTML.IndexOf("</table>");

            sourceHTML = sourceHTML.Remove(idx);

            // for debug
            System.IO.File.WriteAllText(@"C:\Users\riciu\Desktop\Titanic Note.txt", sourceHTML);

            return 0;
        }
    }
}
