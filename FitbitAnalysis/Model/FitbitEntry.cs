using System;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    /// <summary>
    ///     Represents a fitbit entry
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
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets the steps.
        /// </summary>
        /// <value>
        ///     The steps.
        /// </value>
        public int Steps { get; set; }

        /// <summary>
        ///     Gets the distance.
        /// </summary>
        /// <value>
        ///     The distance.
        /// </value>
        public double Distance { get; set; }

        /// <summary>
        ///     Gets the calories burned.
        /// </summary>
        /// <value>
        ///     The calories burned.
        /// </value>
        public int CaloriesBurned { get; set; }

        /// <summary>
        ///     Gets the floors.
        /// </summary>
        /// <value>
        ///     The floors.
        /// </value>
        public int Floors { get; set; }

        /// <summary>
        ///     Gets the activity calories.
        /// </summary>
        /// <value>
        ///     The activity calories.
        /// </value>
        public int ActivityCalories { get; set; }

        public TimeSpan ActiveMinutes { get; }

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
        /// <param name="minutes">The minutes.</param>
        /// <exception cref="ArgumentException">
        ///     Date can not be  null or occur after the current date
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
            int activityCalories, TimeSpan minutes)
        {
            if (date == null)
            {
                throw new ArgumentException("Date can not be null");
            }
            if (minutes == null)
            {
                throw new ArgumentException("Time can not be null");
            }
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date can not be after today");
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
            this.ActiveMinutes = minutes;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FitbitEntry" /> class.
        /// </summary>
        public FitbitEntry() : this(new DateTime(), 0, 0, 0, 0, 0, new TimeSpan())
        {
        }

        #endregion
    }
}