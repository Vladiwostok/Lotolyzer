using System.Data;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Class used for doing the analysis for the NumberTable
    /// </summary>
    class NumberAnalysis
    {
        // The array to be inserted into the NumberTable
        public int[,] NumberRow;

        // The last draw number
        public int LastDraw;

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public NumberAnalysis()
        {
            NumberRow = new int[50, 6];

            for (int idx = 1; idx < 50; idx++)
                NumberRow[idx, 0] = idx;

            LastDraw = 0;
        }

        #endregion

        /// <summary>
        /// Updates the analysis array with the given numbers
        /// </summary>
        /// <param name="Row">The data row based on MainTable</param>
        public void Update(DataRow Row)
        {
            // Update the last draw
            LastDraw++;

            // Update the last delay for all numbers, this will be taken into account
            // when calculating the delays for the current draw
            for (int idx = 1; idx < 50; idx++)
                NumberRow[idx, 2]++;

            for(int idx = 2; idx < 8; idx++)
            {
                // Get the next number
                int num = (byte)Row[idx];

                // Update its frequency
                NumberRow[num, 1]++;

                // Remove the extra occurence from the last delay
                NumberRow[num, 2]--;

                // Update its biggest delay
                if ((int)NumberRow[num, 2] > (int)NumberRow[num, 3])
                    NumberRow[num, 3] = NumberRow[num, 2];

                // Update the sum of the delays
                NumberRow[num, 4] += NumberRow[num, 2];
                // and the count of the delay
                NumberRow[num, 5]++;

                // Update its last delay
                NumberRow[num, 2] = 0;
            }

            // Update the average delay for all numbers
            // for (int idx = 1; idx < 50; idx++)
                // NumberRow[idx, 4] = (double)NumberRow[idx, 1] / lastDraw;
        }
    }
}
