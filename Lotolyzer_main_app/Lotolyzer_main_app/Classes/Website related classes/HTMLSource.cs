using System.Net;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Downloads the html source of a web page
    /// </summary>
    class HTMLSource
    {
        /// <summary>
        /// Downloads the html content of a web page in a string
        /// </summary>
        /// <param name="SiteURL">The URL to download from</param>
        /// <returns>The string containing the HTML content</returns>
        public static string DownloadAsString(string SiteURL)
        {
            // Puts all the html source in the string
            string source = new WebClient().DownloadString(SiteURL);

            return source;
        }

        // In the future, maybe DownloadAsFile();
    }
}
