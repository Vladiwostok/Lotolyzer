using System.Data;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Class used for doing the analysis for the DrawTable
    /// </summary>
    class DrawAnalysis
    {
        // The array to be inserted into the DrawTable
        public object[] DrawRow;

        #region Constructors

        /// <summary>
        /// Default constructor based on a data row from MainTable
        /// </summary>
        /// <param name="row">The row to copy the starting items</param>
        public DrawAnalysis(DataRow Row)
        {
            DrawRow = new object[29];

            for (int idx = 0; idx < 8; idx++)
                DrawRow[idx] = Row[idx];
        }

        #endregion

        /// <summary>
        /// Calculates the fields from DrawTable in the current draw
        /// </summary>
        public void Analyze()
        {
            // The number array which will be used for the calculations
            int[] nums = new int[6];

            // The control sum
            int sum = 0;

            // The count of the even numbers
            int even = 0;

            // The count of the numbers smaller than the middle number ( <= 25)
            int half = 0;

            // Occurences on draw lines
            int[] linenum = new int[5];

            // Occurences on draw columns
            int[] colnum = new int[10];

            // Copy all the numbers from the object list into the number array
            // and do the basic calculations
            for (int idx = 0; idx < 6; idx++)
            {
                // Set the number
                nums[idx] = (byte)DrawRow[idx + 2];

                // Update the sum
                sum += nums[idx];

                // Update the occurence on the line
                linenum[(nums[idx] - 1) / 10]++;

                // Update the occurence on the column
                colnum[nums[idx] % 10]++;

                // Update the even number count
                if (nums[idx] % 2 == 0)
                    even++;

                // Update the half number count
                if (nums[idx] <= 25)
                    half++;
            }

            // Calculate the control sum
            while(sum > 9)
            {
                int aux = 0;

                while(sum > 0)
                {
                    aux += sum % 10;
                    sum /= 10;
                }

                sum = aux;
            }

            // Sort the number array so it can be used to check if there are consecutive numbers
            System.Array.Sort(nums);

            // The array for storing consecutive number events
            int[] cons = new int[3];

            // The size of the current consecutive sequence
            int currentcons = 0;

            // Check for consecutive numbers
            for (int idx = 0; idx < 5; idx++)
            {
                if (nums[idx] + 1 == nums[idx + 1])
                    currentcons++;
                else
                {
                    if (currentcons > 0)
                        cons[currentcons - 1]++;

                    currentcons = 0;
                }
            }

            // Don't forget about the last number, it could also be a part of a sequence that doesn't get tested
            if(currentcons > 0)
                cons[currentcons - 1]++;

            // The current empty position in the object array; by default it should be 8,
            // (after the id, date and the 6 numbers), as this function is called after the first
            // initialisations happen
            int currentpos = 8;

            // Now copy all the calculated data into the object array

            DrawRow[currentpos++] = even;

            DrawRow[currentpos++] = half;

            for (int idx = 0; idx < 5; idx++)
                DrawRow[currentpos++] = linenum[idx];

            for (int idx = 0; idx < 10; idx++)
                DrawRow[currentpos++] = colnum[idx];

            DrawRow[currentpos++] = sum;

            for (int idx = 0; idx < 3; idx++)
                DrawRow[currentpos++] = cons[idx];
        }
    }
}
