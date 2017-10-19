using System;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitEntry
    {
        #region Properties
        public DateTime Date { get; }
        public int Steps { get; }
        public double Distance { get; }
        public int CaloriesBurned { get; }
        public int Floors { get; }
        public int ActivityCalories { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FitbitEntry"/> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="steps">The steps.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="caloriesBurned">The calories burned.</param>
        /// <param name="floors">The floors.</param>
        /// <param name="activityCalories">The activity calories.</param>
        /// <exception cref="ArgumentException">
        /// Date can not be  null
        /// or
        /// Steps musts be positive
        /// or
        /// Distance musts be positive
        /// or
        /// Steps musts be positive
        /// or
        /// floorCountTracker musts be positive
        /// or
        /// activityCaloriesTracker musts be positive
        /// </exception>
        public FitbitEntry(DateTime date, int steps, double distance, int caloriesBurned, int floors, int activityCalories)
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
    }
}
