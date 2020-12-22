namespace Lotolyzer_main_app
{
    /// <summary>
    /// Class used only for constants related to the Draw URLs
    /// </summary>
    public static class DrawURL
    {
        /// <summary>
        /// The year the draws started to get archived
        /// </summary>
        public static int StartingYear = 1993;

        /// <summary>
        /// Current year (for a quick refresh only the current year archive is checked)
        /// </summary>
        public static int CurrentYear = System.DateTime.Now.Year;

        /// <summary>
        /// The Base URL of the archive site;
        /// Just add the desired year at the end of it to get to that year's draw
        /// </summary>
        public static string BaseDrawArchiveURL = "http://noroc-chior.ro/Loto/6-din-49/arhiva-rezultate.php?Y=";
    }
}
