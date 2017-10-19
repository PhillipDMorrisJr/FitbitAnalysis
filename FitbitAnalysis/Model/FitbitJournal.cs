using System;
using System.Collections.Generic;
using System.Linq;

namespace FitbitAnalysis_Phillip_Morris.Model
{
    /// <summary>
    ///     Manages the category by arranging various collections objects.
    /// </summary>
    public class FitbitJournal
    {
        #region Data members

        private readonly List<FitbitEntry> fitbitEntries;

        #endregion

        #region Properties

        public int MinSteps => this.fitbitEntries.Min(entry => entry.Steps);

        public int MaxSteps => this.fitbitEntries.Max(entry => entry.Steps);

        public double AverageSteps => this.fitbitEntries.Average(entry => entry.Steps);
        public DateTime FirstEntryDate => this.getAllEntriesOrderedByDate()[0].Date;
        public DateTime LastEntryDate => this.getAllEntriesOrderedByDate()[this.fitbitEntries.Count - 1].Date;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FitbitJournal" /> class.
        /// </summary>
        public FitbitJournal()
        {
            this.fitbitEntries = new List<FitbitEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the entry.
        /// </summary>
        /// <param name="fitbitEntry"> The entry to be added to fitbit collection</param>
        public void AddEntry(FitbitEntry fitbitEntry)
        {
            this.fitbitEntries.Add(fitbitEntry ?? throw new ArgumentException("Fitbit must not be null"));
        }

        /// <summary>
        ///     Finds the date based on stepTracker.
        /// </summary>
        /// <param name="steps">The step count.</param>
        /// <returns></returns>
        public DateTime FindDateBasedOnSteps(int steps)
        {
            var currentDate = this.fitbitEntries.Find(fitbitEntry => fitbitEntry.Steps == steps).Date;
            return currentDate;
        }

        /// <summary>
        ///     Counts the days with stepTracker between.
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsBetween(int lowerBound, int upperBound)
        {
            int count;
            count = this.fitbitEntries.Count(
                fitbitEntry => fitbitEntry.Steps >= lowerBound && fitbitEntry.Steps <= upperBound);
            return count;
        }

        /// <summary>
        ///     Counts the days with stepTracker over.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsOver(int bound)
        {
            var count = this.fitbitEntries.Count(fitbitEntry => fitbitEntry.Steps >= bound);
            return count;
        }

        /// <summary>
        ///     Counts the days with stepTracker less than.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns></returns>
        public int CountDaysWithStepsLessThan(int bound)
        {
            var count = this.fitbitEntries.Count(fitbitEntry => fitbitEntry.Steps <= bound);
            return count;
        }

        /// <summary>
        ///     Gets the ordered dateTracker by month.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<FitbitEntry> GetEntriesOrderedDatesByMonthAndYear(int month, int year)
        {
            var copyFitbitEntries = this.fitbitEntries;

            var fitbitEntriesInYear = copyFitbitEntries.FindAll(fitbitEntry => fitbitEntry.Date.Year == year);
            var fitbitEntriesInMonth = fitbitEntriesInYear.FindAll(fitbitEntry => fitbitEntry.Date.Month == month);
            var fitbitEntriesorderedByDates = fitbitEntriesInMonth.OrderBy(fitbitEntry => fitbitEntry.Date);

            return fitbitEntriesorderedByDates.ToList();
        }

        /// <summary>
        ///     Clears all trackers for the instance of this class.
        /// </summary>
        public void ClearEntries()
        {
            this.fitbitEntries.Clear();
        }

        /// <summary>
        ///     Gets the entries associated with the date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public FitbitEntry GetEntryByDate(DateTime date)
        {
            return this.fitbitEntries.Find(fitbitEntry => fitbitEntry.Date.Equals(date));
        }

        /// <summary>
        ///     Checks if this instance is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this.fitbitEntries.Count == 0;
        }

        /// <summary>
        ///     Determines whether the specified date contains date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        ///     <c>true</c> if the specified date contains date; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsDate(DateTime date)
        {
            return this.fitbitEntries.Exists(fitbitEntry => fitbitEntry.Date.Equals(date));
        }

        /// <summary>
        ///     Gets all entries ordered by date.
        /// </summary>
        /// <returns></returns>
        private List<FitbitEntry> getAllEntriesOrderedByDate()
        {
            var fitbitEntriesOrderedByDates = this
                .fitbitEntries.OrderBy(fitbitEntry => fitbitEntry.Date.Year)
                .ThenBy(fitbitEntry => fitbitEntry.Date.Month);

            return fitbitEntriesOrderedByDates.ToList();
        }

        /// <summary>
        ///     Replaces the matching date entries.
        /// </summary>
        /// <param name="fitbitEntry">The fitbit entry.</param>
        public void ReplaceMatchingDateEntries(FitbitEntry fitbitEntry)
        {
            this.fitbitEntries.RemoveAll(entry => entry.Date.Equals(fitbitEntry.Date));
            this.AddEntry(fitbitEntry);
        }

        /// <summary>
        ///     Counts the per month and year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public int CountPerMonthAndYear(int month, int year)
        {
            var entries = this.fitbitEntries.FindAll(entry => entry.Date.Month == month && entry.Date.Year == year);
            return entries.Count;
        }

        #endregion

        /// <summary>
        /// Gets the entries by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// a Journal containing the yearly entries for the year in the year parameter
        /// </returns>
        public FitbitJournal GetEntriesByYear(int year)
        {
            var entries = this.fitbitEntries.FindAll(entry => entry.Date.Year == year);
            FitbitJournal yearlyJournal = new FitbitJournal();

            foreach (var entry in entries)
            {
                yearlyJournal.AddEntry(entry);
            }
            return yearlyJournal;
        }
    }
}