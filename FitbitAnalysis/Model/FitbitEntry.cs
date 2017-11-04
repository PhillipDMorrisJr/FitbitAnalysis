using System;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    /// <summary>
    /// </summary>
    public class FitbitEntry
    {
        #region Properties

        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <value>
        ///     The date.
        /// </value>
        public DateTime Date { get; }

        /// <summary>
        ///     Gets the steps.
        /// </summary>
        /// <value>
        ///     The steps.
        /// </value>
        public int Steps { get; }

        /// <summary>
        ///     Gets the distance.
        /// </summary>
        /// <value>
        ///     The distance.
        /// </value>
        public double Distance { get; }

        /// <summary>
        ///     Gets the calories burned.
        /// </summary>
        /// <value>
        ///     The calories burned.
        /// </value>
        public int CaloriesBurned { get; }

        /// <summary>
        ///     Gets the floors.
        /// </summary>
        /// <value>
        ///     The floors.
        /// </value>
        public int Floors { get; }

        /// <summary>
        ///     Gets the activity calories.
        /// </summary>
        /// <value>
        ///     The activity calories.
        /// </value>
        public int ActivityCalories { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FitbitEntry" /> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="steps">The steps.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="caloriesBurned">The calories burned.</param>
        /// <param name="floors">The floors.</param>
        /// <param name="activityCalories">The activity calories.</param>
        /// <exception cref="ArgumentException">
        ///     Date can not be  null
        ///     or
        ///     Steps musts be positive
        ///     or
        ///     Distance musts be positive
        ///     or
        ///     Steps musts be positive
        ///     or
        ///     floorCountTracker musts be positive
        ///     or
        ///     activityCaloriesTracker musts be positive
        /// </exception>
        public FitbitEntry(DateTime date, int steps, double distance, int caloriesBurned, int floors,
            int activityCalories)
        {
            if (date == null)
            {
                throw new ArgumentException("Date can not be  null");
            }
            if (steps < 0)
            {
                throw new ArgumentException("Steps musts be positive");
            }
            if (distance < 0)
            {
                throw new ArgumentException("Distance musts be positive");
            }
            if (caloriesBurned < 0)
            {
                throw new ArgumentException("Calories burned musts be positive");
            }
            if (floors < 0)
            {
                throw new ArgumentException("Floors musts be positive");
            }
            if (activityCalories < 0)
            {
                throw new ArgumentException("Activity Calories musts be positive");
            }

            this.Date = date;
            this.Steps = steps;
            this.Distance = distance;
            this.CaloriesBurned = caloriesBurned;
            this.Floors = floors;
            this.ActivityCalories = activityCalories;
        }

        #endregion
    }
}