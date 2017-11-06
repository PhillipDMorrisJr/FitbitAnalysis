using System;
using FitbitAnalysis_Phillip_Morris.Model;

namespace FitbitAnalysis_Phillip_Morris.Utility.IO
{
    /// <summary>
    ///     Parses FIle
    /// </summary>
    public static class Parser
    {
        #region Methods

        /// <summary>
        ///     Parses the CSV.
        ///     Input by index
        ///     0: DateTime date
        ///     1: int caloriesBurned
        ///     2: int steps
        ///     3: double distance
        ///     4: int floors
        ///     5: int activityCalories
        ///     6: TimeSpan activeMinutes
        /// </summary>
        /// <param name="input">The input to be parsed.</param>
        /// <Exceptions>FormatException, ArgumentException</Exceptions>
        /// <returns></returns>
        public static FitbitEntry ParseCsv(string[] input)
        {
            if (input == null)
            {
                throw new ArgumentException("input cannot be null");
            }
            var date = DateTime.Parse(input[0]);
            var caloriesBurned = int.Parse(input[1]);
            var steps = int.Parse(input[2]);
            var distance = double.Parse(input[3]);
            var floors = int.Parse(input[4]);
            var activityCalories = int.Parse(input[5]);
            var activeMinutes = parseActiveMinutes(input);
            return new FitbitEntry(date, steps, distance, caloriesBurned, activityCalories, floors, activeMinutes);
        }

        /// <summary>
        ///     Parses the active minutes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static TimeSpan parseActiveMinutes(string[] input)
        {
            var timeLine = input[6];
            var timeInMinutes = int.Parse(timeLine);

            var activeMinutes = MinuteConverter.ConvertMinutesToTimeSpan(timeInMinutes);
            return activeMinutes;
        }

        #endregion
    }
}